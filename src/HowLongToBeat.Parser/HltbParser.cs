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

    private string? _token;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private static readonly Regex ScriptSearchRegex = new(@"<script\s+src=[""'][^""'>]*_app[^""'>]*[""'][^>]*><\/script>");
    private static readonly Regex ScriptSrcSearchRegex = new(@"src="".{1,}?""");
    private static readonly Regex TokenSearchRegex = new(@"\/api\/lookup\/""(?:\.concat\(""[^""]*""\))*");
    private static readonly Regex TokenExtractRegex = new(@"""\w{1,}?""");

    public async Task<SearchResponse> Search(SearchRequest request, Context? context, CancellationToken cancellationToken)
    {
        if (context == null)
            await AcquireUserToken(cancellationToken);
        else
            _token = context.Token;

        var response = await _client.SearchGames(_token!, request, cancellationToken);
        
        return response;
    }

    private async Task AcquireUserToken(CancellationToken cancellationToken)
    {
        if (_token == null)
        {
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var token = await TryGetUserToken(cancellationToken);

                if (token == null)
                    throw new Exception("Failed to get user token, maybe server change token mechanism");

                _token ??= token;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }

    public async Task<string?> TryGetUserToken(CancellationToken cancellationToken)
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

            var tokenPartsMatches = TokenExtractRegex.Matches(tokenMatch.Value);

            if (tokenPartsMatches.Count == 0)
                continue;

            return string.Join("", tokenPartsMatches.Select(match => match.Value.Trim('"')));
        }

        return null;
    }

    public void Dispose()
    {
        _semaphore.Dispose();
    }
}