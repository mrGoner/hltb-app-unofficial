using HowLongToBeat.App.Resources;

namespace HowLongToBeat.App.Models;

public record Game(string Name, Uri? ImageUrl, TimeSpan MainDuration, TimeSpan ExtraDuration, TimeSpan PerfectDuration, int ReleaseYear)
{
    public string MainDurationString => ConvertToTimeString(MainDuration);
    public string ExtraDurationString => ConvertToTimeString(ExtraDuration);
    public string PerfectDurationString => ConvertToTimeString(PerfectDuration);

    private static string ConvertToTimeString(TimeSpan timeSpan)
    {
        if (timeSpan.TotalMinutes == 0)
            return "-";

        if (timeSpan.TotalMinutes < 60)
            return $"{Math.Round(timeSpan.TotalMinutes, 0, MidpointRounding.AwayFromZero)} {AppResources.Minutes}";

        if (timeSpan.Minutes is > 15 and < 45)
            return $"{(timeSpan.Days == 0 ? timeSpan.Hours : timeSpan.Days * 24 + timeSpan.Hours)}\u00bd {AppResources.Hours}";

        return $"{Math.Round(timeSpan.TotalHours, 0, MidpointRounding.AwayFromZero)} {AppResources.Hours}";
    }
}