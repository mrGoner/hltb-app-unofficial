using HowLongToBeat.Parser;
using HowLongToBeat.Parser.Models.Requests;

var parser = new HltbParser();

var request = new SearchRequest(SearchType.Games, ["Game"], 1, 20, SearchOptions.Default, true);

var context = await parser.GetSearchContext(CancellationToken.None);

var response = await parser.Search(request, context, CancellationToken.None);

foreach (var game in response.Games)
    Console.WriteLine(game);

Console.ReadKey();