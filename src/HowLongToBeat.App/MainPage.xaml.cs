using CommunityToolkit.Maui.Extensions;
using FFImageLoading;
using HowLongToBeat.App.ViewModels;
using HowLongToBeat.Parser;
using HowLongToBeat.Parser.Models.Requests;
using MetroLog.Maui;
using Microsoft.Extensions.Logging;

namespace HowLongToBeat.App;

public partial class MainPage : ContentPage
{
    private readonly LogController _logController;
    
    public MainPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        var imageService = serviceProvider.GetRequiredService<IImageService>();
        
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");
        imageService.Configuration.HttpClient = httpClient;
        
        _logController = new LogController
        {
            IsShakeEnabled = false
        };

        BindingContext = new MainViewModel(
            hltbParser: serviceProvider.GetRequiredService<HltbParser>(),
            showFilterPageFunc: ShowFilterPageFunc,
            displayAlertWithCaptionFunc: DisplayAlertWithCaption,
            logger: serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("SearchPage"));
    }

    private Task<bool> DisplayAlertWithCaption(DisplayAlertParams alertParams) =>
        DisplayAlertAsync(alertParams.Title, alertParams.Message, alertParams.AcceptText, alertParams.CancelText);

    private async Task<GamesFilter?> ShowFilterPageFunc(GamesFilter filter)
    {
        var popupResult = await this.ShowPopupAsync<GamesFilter>(new FilterPage(filter));

        return popupResult.Result;
    }

    private void ShowAboutPage(object? sender, EventArgs e)
    {
        this.ShowPopup(new AboutPage());
    }

    private void ShowLogsPage(object? sender, EventArgs e)
    {
        _logController.GoToLogsPageCommand.Execute(null);
    }
}