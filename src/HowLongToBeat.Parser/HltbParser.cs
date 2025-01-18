using System.Text.Json;
using System.Text.RegularExpressions;
using HowLongToBeat.Parser.JsonConverters;
using HowLongToBeat.Parser.Models.Requests;
using HowLongToBeat.Parser.Models.Responses;
using Refit;

namespace HowLongToBeat.Parser;

public class HltbParser : IDisposable
{
    private readonly IHowLongToBeatClient _client = RestService.For<IHowLongToBeatClient>("https://howlongtobeat.com",
        new RefitSettings(new SystemTextJsonContentSerializer(new JsonSerializerOptions
        {
            Converters = {new EnumToStringConverterFactory()}
        })));

    private SearchContext? _searchContext;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private static readonly Regex ScriptSearchRegex = new(@"<script\s+src=[""'][^""'>]*_app[^""'>]*[""'][^>]*><\/script>");
    private static readonly Regex ScriptSrcSearchRegex = new(@"src="".{1,}?""");
    private static readonly Regex TokenSearchRegex = new(@"fetch\(""\/api\/[^""]*""(?:\.concat\(""[^""]*""\)){2}");
    private static readonly Regex TokenExtractRegex = new(@"""\w{1,}?""");
    private static readonly Regex SearchApiPathExtractRegex = new(@"fetch\(""(\/api\/[^""]+)");

    public async Task<SearchResponse> Search(SearchRequest request, SearchContext? context, CancellationToken cancellationToken)
    {
        if (context == null)
            await AcquireUserToken(cancellationToken);

        var currentContext = context ?? _searchContext ?? throw new ArgumentException("Search context is null");

        var response = await _client.SearchGames(currentContext.SearchPath, currentContext.Token, request, cancellationToken);
        
        return response;
    }

    private async Task AcquireUserToken(CancellationToken cancellationToken)
    {
        if (_searchContext == null)
        {
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var searchContext = await TryGetSearchContext(cancellationToken);

                if (searchContext == null)
                    throw new Exception("Failed to get user token, maybe server change token mechanism");

                _searchContext ??= searchContext;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }

    public async Task<SearchContext?> TryGetSearchContext(CancellationToken cancellationToken)
    {
        var pageContent = await _client.GetMain(cancellationToken);

        foreach (Match scriptMatch in ScriptSearchRegex.Matches(pageContent))
        {
            var scriptPathMatch = ScriptSrcSearchRegex.Match(scriptMatch.Value);

            if (!scriptPathMatch.Success)
                continue;

            var scriptPath = scriptPathMatch.Value.Replace(@"src=""/", string.Empty).Trim('"');

            var scriptContent = await _client.LoadScript(scriptPath, cancellationToken);

            var tokenMatch = TokenSearchRegex.Match(scriptContent);

            if (!tokenMatch.Success)
                continue;

            var apiPathMatch = SearchApiPathExtractRegex.Match(tokenMatch.Value);

            if (!apiPathMatch.Success)
                continue;

            var apiPath = apiPathMatch.Groups[1].Value.Trim('/');

            var tokenPartsMatches = TokenExtractRegex.Matches(tokenMatch.Value);

            if (tokenPartsMatches.Count == 0)
                continue;

            var token = string.Join("", tokenPartsMatches.Select(match => match.Value.Trim('"')));

            return new SearchContext(token, apiPath);
        }

        return null;
    }

    public void Dispose()
    {
        _semaphore.Dispose();
    }
}