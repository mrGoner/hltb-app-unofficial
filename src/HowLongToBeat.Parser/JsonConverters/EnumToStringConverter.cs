using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser.JsonConverters;

internal sealed class EnumToStringConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();

        foreach (var field in typeToConvert.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attribute = field.GetCustomAttribute<EnumMemberAttribute>();

            if (attribute != null && attribute.Value == stringValue)
                return (T) (field.GetValue(null) ?? throw new Exception($"Failed to convert {stringValue} to {typeToConvert}"));

            if (field.Name.Equals(stringValue, StringComparison.OrdinalIgnoreCase))
                return (T) (field.GetValue(null) ?? throw new Exception($"Failed to convert {stringValue} to {typeToConvert}"));
        }

        throw new JsonException($"Unable to map '{stringValue}' to enum {typeToConvert.Name}");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var field = typeof(T).GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<EnumMemberAttribute>();

        writer.WriteStringValue(attribute != null ? attribute.Value : value.ToString());
    }
}
