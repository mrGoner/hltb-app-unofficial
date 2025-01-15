using System.Runtime.Serialization;

namespace HowLongToBeat.Parser.Models.Requests;

public enum SearchType
{
    [EnumMember(Value = "games")]
    Games
}