using System.Text.Json;
using System.Text.Json.Serialization;
using HowLongToBeat.Parser.JsonConverters;
using HowLongToBeat.Parser.Models.Requests;
using HowLongToBeat.Parser.Models.Responses;
using Refit;

namespace HowLongToBeat.Parser;

public class HltbParser
{
    private readonly IHowLongToBeatClient _client = RestService.For<IHowLongToBeatClient>("https://howlongtobeat.com",
        new RefitSettings(new SystemTextJsonContentSerializer(new JsonSerializerOptions
        {
            Converters = {new EnumToStringConverterFactory()}
        })));

    public async Task<SearchResponse> Search(SearchRequest request, SearchContext context, CancellationToken cancellationToken)
    {
        var internalRequest = new SearchRequestWithAdditionalAuthData(request.SearchType, request.SearchTerms,
            request.SearchPage, request.Size, request.SearchOptions, request.UserCache)
        {
            Extra =
            {
                [context.AdditionalData.Key] = context.AdditionalData.Value
            }
        };

        var response = await _client.SearchGames(context.Token, context.AdditionalData.Key,
            context.AdditionalData.Value, internalRequest, cancellationToken);

        return response;
    }

    public async Task<SearchContext> GetSearchContext(CancellationToken cancellationToken)
    {
        var currentTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var tokenResponse = await _client.GetAuthToken(currentTimestampMs, cancellationToken);

        if (!tokenResponse.IsSuccessful || tokenResponse.Content is null)
            throw new Exception("Failed to get user token, maybe server change token mechanism", tokenResponse.Error);

        var tokenResult = tokenResponse.Content;
        var additionalData = new KeyValuePair<string, string>(tokenResult.AdditionalKey, tokenResult.AdditionalValue);

        return new SearchContext(additionalData, tokenResult.Token);
    }

    private record SearchRequestWithAdditionalAuthData(
        SearchType SearchType,
        string[] SearchTerms,
        int SearchPage,
        int Size,
        SearchOptions SearchOptions,
        bool UserCache) : SearchRequest(SearchType, SearchTerms, SearchPage, Size, SearchOptions, UserCache)
    {
        
        [property:JsonExtensionData]
        public Dictionary<string, object> Extra {get; set;} = new();
    }
}