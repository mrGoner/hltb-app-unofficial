using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser;

public record TokenResult(
    [property: JsonPropertyName("hpKey")] 
    string AdditionalKey,
    [property: JsonPropertyName("hpVal")] 
    string AdditionalValue,
    [property: JsonPropertyName("token")] 
    string Token);