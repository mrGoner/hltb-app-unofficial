using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HowLongToBeat.Parser.Models.Requests;

public record UsersFilter(
    [property: JsonPropertyName("sortCategory")]
    UsersFilter.SortCategoryType SortCategory)
{
    public static UsersFilter Default => new(SortCategoryType.PostCount);
    public enum SortCategoryType
    {
        [EnumMember(Value = "postcount")]
        PostCount
    }
}