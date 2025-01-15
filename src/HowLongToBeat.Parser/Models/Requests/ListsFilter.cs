using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser.Models.Requests;

public record ListsFilter(    
    [property: JsonPropertyName("sortCategory")]
    ListsFilter.SortCategoryType SortCategory)
{
    public static ListsFilter Default => new(SortCategoryType.Follows);
    public enum SortCategoryType
    {
        [EnumMember(Value = "follows")]
        Follows
    }
}