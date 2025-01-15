using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser.Models.Responses;

public record SearchResponse(
    [property: JsonPropertyName("pageCurrent")]
    int PageCurrent,
    [property: JsonPropertyName("pageTotal")]
    int PageTotal,
    [property: JsonPropertyName("pageSize")]
    int PageSize,
    [property: JsonPropertyName("data")]
    Game[] Games);