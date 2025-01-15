using CommunityToolkit.Maui.Views;
using FFImageLoading;
using HowLongToBeat.App.ViewModels;
using HowLongToBeat.Parser.Models.Requests;

namespace HowLongToBeat.App;

public partial class MainPage : ContentPage
{
    public MainPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        var imageService = serviceProvider.GetRequiredService<IImageService>();
        
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");
        imageService.Configuration.HttpClient = httpClient;

        BindingContext = new MainViewModel(ShowFilterPageFunc, DisplayAlertWithCaption);
    }

    private Task<bool> DisplayAlertWithCaption(DisplayAlertParams alertParams) =>
        DisplayAlert(alertParams.Title, alertParams.Message, alertParams.AcceptText, alertParams.CancelText);

    private async Task<GamesFilter?> ShowFilterPageFunc(GamesFilter filter)
    {
        var result = await this.ShowPopupAsync(new FilterPage(filter));

        return result as GamesFilter;
    }

    private void ShowAboutPage(object? sender, EventArgs e)
    {
        this.ShowPopup(new AboutPage());
    }
}