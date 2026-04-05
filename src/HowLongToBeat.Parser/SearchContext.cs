namespace HowLongToBeat.Parser;

public record SearchContext(SearchContext.Data AdditionalData, string Token)
{
    public record Data(string Key, string Value);
}