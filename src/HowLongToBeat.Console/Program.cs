using HowLongToBeat.Parser;
using HowLongToBeat.Parser.Models.Requests;

using var parser = new HltbParser();

var request = new SearchRequest(SearchType.Games, ["Game"], 1, 20, SearchOptions.Default, true);

var response = await parser.Search(request, null, CancellationToken.None);

foreach (var game in response.Games)
    Console.WriteLine(game);

Console.ReadKey();