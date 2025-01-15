using System.Text.Json;
using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser.JsonConverters;

internal sealed class EnumToStringConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }
    
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return Activator.CreateInstance(typeof(EnumToStringConverter<>).MakeGenericType(typeToConvert)) as JsonConverter;
    }
}