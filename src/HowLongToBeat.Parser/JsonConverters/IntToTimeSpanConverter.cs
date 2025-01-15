using System.Text.Json;
using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser.JsonConverters;

internal sealed class IntToTimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var timeInSeconds = reader.GetInt32();

        return TimeSpan.FromSeconds(timeInSeconds);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue((int) value.TotalSeconds);
    }
}