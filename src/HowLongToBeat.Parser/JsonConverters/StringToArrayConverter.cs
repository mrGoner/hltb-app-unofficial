using System.Text.Json;
using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser.JsonConverters;

internal sealed class StringToArrayConverter : JsonConverter<string[]>
{
    public override string[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString()?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    }

    public override void Write(Utf8JsonWriter writer, string[] value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(string.Join(" ", value));
    }
}