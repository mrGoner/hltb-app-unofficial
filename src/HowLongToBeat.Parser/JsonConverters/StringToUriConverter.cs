using System.Text.Json;
using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser.JsonConverters;

internal sealed class StringToUriConverter : JsonConverter<Uri?>
{
    private const string Host = "https://howlongtobeat.com/games/";
    private const string WidthQuery = "?width=100";
    public override Uri? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var uriString = reader.GetString();

        return uriString == null ? null : new Uri($"{Host}{uriString}{WidthQuery}");
    }

    public override void Write(Utf8JsonWriter writer, Uri? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString().Replace(Host, string.Empty).Replace(WidthQuery, string.Empty));
    }
}