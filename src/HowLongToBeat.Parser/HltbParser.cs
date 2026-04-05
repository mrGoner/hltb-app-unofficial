using System.Text.Json;
using System.Text.Json.Serialization;
using HowLongToBeat.Parser.JsonConverters;
using HowLongToBeat.Parser.Models.Requests;
using HowLongToBeat.Parser.Models.Responses;
using Microsoft.Extensions.Logging;
using Refit;

namespace HowLongToBeat.Parser;

public class HltbParser(ILogger logger)
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

        try
        {
            var response = await _client.SearchGames(context.Token, context.AdditionalData.Key,
                context.AdditionalData.Value, internalRequest, cancellationToken);
            
            return response;
        }
        catch (ApiException apiException)
        {
            var requestContent = apiException.RequestMessage.Content == null
                ? string.Empty
                : await apiException.RequestMessage.Content.ReadAsStringAsync(cancellationToken);

            logger.LogError(apiException, "Failed to perform search query: {Query}", requestContent);

            throw;
        }
    }

    public async Task<SearchContext> GetSearchContext(CancellationToken cancellationToken)
    {
        var currentTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var tokenResponse = await _client.GetAuthToken(currentTimestampMs, cancellationToken);

        if (!tokenResponse.IsSuccessful)
        {
            logger.LogError(tokenResponse.Error, "Failed to get auth token. StatusCode: {StatusCode}", tokenResponse.StatusCode);
            
            throw new Exception("Failed to get user token, maybe server change token mechanism", tokenResponse.Error);
        }

        var tokenResult = tokenResponse.Content;
        var additionalData = new SearchContext.Data(tokenResult.AdditionalKey, tokenResult.AdditionalValue);
        
        logger.LogDebug("Got success token data: {Data}", tokenResult);

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