using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using HowLongToBeat.Parser;
using MetroLog.MicrosoftExtensions;
using MetroLog.Operators;
using Microsoft.Extensions.Logging;

namespace HowLongToBeat.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseFFImageLoading()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        //logging
        builder.Logging
            .SetMinimumLevel(LogLevel.Debug)
            .AddConsoleLogger(options =>
            {
                options.MinLevel = LogLevel.Information;
                options.MaxLevel = LogLevel.Critical;
            })
            .AddInMemoryLogger(options =>
            {
                options.MaxLines = 1024;
#if DEBUG
                options.MinLevel = LogLevel.Debug;
#else
                options.MinLevel = LogLevel.Information;
#endif
                options.MaxLevel = LogLevel.Critical;
            })
            .AddStreamingFileLogger(options =>
            {
                options.RetainDays = 2;
                options.FolderPath = Path.Combine(FileSystem.CacheDirectory, "MetroLogs");
            });

        builder.Services.AddScoped<HltbParser>(provider =>
            new HltbParser(provider.GetRequiredService<ILoggerFactory>().CreateLogger("Parser")));
        builder.Services.AddSingleton(LogOperatorRetriever.Instance);
        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}