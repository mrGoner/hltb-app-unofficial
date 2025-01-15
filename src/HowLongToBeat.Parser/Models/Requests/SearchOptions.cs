using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser.Models.Requests;

public record SearchOptions(
    [property: JsonPropertyName("games")] 
    GamesFilter Games,
    [property: JsonPropertyName("users")] 
    UsersFilter UsersFilter,
    [property: JsonPropertyName("lists")] 
    ListsFilter ListsFilter,
    [property: JsonPropertyName("filter")] 
    string Filter,
    [property: JsonPropertyName("sort")] 
    int Sort,
    [property: JsonPropertyName("randomizer")]
    int Randomizer)
{
    public static SearchOptions Default => new(GamesFilter.Default, UsersFilter.Default, ListsFilter.Default, string.Empty, 0, 0);
}