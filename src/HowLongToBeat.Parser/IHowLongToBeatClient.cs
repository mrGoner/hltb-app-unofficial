using HowLongToBeat.Parser.Models.Requests;
using HowLongToBeat.Parser.Models.Responses;
using Refit;

namespace HowLongToBeat.Parser;

[Headers(
    "Referer: https://howlongtobeat.com",
    "User-Agent: Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36")]
internal interface IHowLongToBeatClient
{
    [Post("/api/search")]
    public Task<SearchResponse> SearchGames([Header("x-auth-token")] string token, SearchRequest request,
        CancellationToken cancellationToken);

    [Get("/api/search/init?t={timestamp}")]
    public Task<ApiResponse<TokenResult>> GetAuthToken([AliasAs("timestamp")] long timestampMs, CancellationToken cancellationToken);
}