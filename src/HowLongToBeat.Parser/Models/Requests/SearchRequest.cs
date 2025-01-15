using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser.Models.Requests;

public record SearchRequest(
    [property: JsonPropertyName("searchType")]
    SearchType SearchType,
    [property: JsonPropertyName("searchTerms")]
    string[] SearchTerms,
    [property: JsonPropertyName("searchPage")]
    int SearchPage,
    [property: JsonPropertyName("size")]
    int Size,
    [property: JsonPropertyName("searchOptions")]
    SearchOptions SearchOptions,
    [property: JsonPropertyName("useCache")]
    bool UserCache);