using HowLongToBeat.Parser.Models.Requests;
using HowLongToBeat.Parser.Models.Responses;
using Refit;

namespace HowLongToBeat.Parser;

[Headers(
    "Referer: https://howlongtobeat.com", 
    "User-Agent: Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36")]
internal interface IHowLongToBeatClient
{
    [Post("/{path}/{token}")]
    [QueryUriFormat(UriFormat.Unescaped)]
    public Task<SearchResponse> SearchGames([AliasAs("path")] string apiPath, [AliasAs("token")] string userToken,
        SearchRequest request,
        CancellationToken cancellationToken);

    [Get("/")]
    public Task<string> GetMain(CancellationToken cancellationToken);

    [Get("/{script}")]
    [QueryUriFormat(UriFormat.Unescaped)]
    public Task<string> LoadScript([AliasAs("script")] string scriptPath, CancellationToken cancellationToken);
}