using System.Globalization;

namespace HowLongToBeat.App.ValueFormatters;

public sealed class TimeSpanFormatter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TimeSpan timeSpan)
            return $"{(timeSpan.Days == 0 ? timeSpan.Hours : timeSpan.Days * 24 + timeSpan.Hours)} hours {timeSpan.Minutes} minutes {timeSpan.Seconds} seconds";
        
        return Binding.DoNothing;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string str && TimeSpan.TryParse(str, out TimeSpan result))
            return result;
        
        return Binding.DoNothing;
    }
}