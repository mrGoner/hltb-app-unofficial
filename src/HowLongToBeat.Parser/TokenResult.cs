using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser;

public record TokenResult([property:JsonPropertyName("token")] string Token);