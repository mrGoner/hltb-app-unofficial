namespace HowLongToBeat.App.Models;

public record EnumValueHolder<T>(T Value, string LocalizedName) where T : Enum;