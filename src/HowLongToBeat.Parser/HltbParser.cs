using System.Text.Json;
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
        var response = await _client.SearchGames(context.Token, request, cancellationToken);

        return response;
    }

    public async Task<SearchContext> GetSearchContext(CancellationToken cancellationToken)
    {
        var currentTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var tokenResponse = await _client.GetAuthToken(currentTimestampMs, cancellationToken);

        if (!tokenResponse.IsSuccessful || tokenResponse.Content is null)
            throw new Exception("Failed to get user token, maybe server change token mechanism", tokenResponse.Error);

        return new SearchContext(tokenResponse.Content.Token);
    }
}