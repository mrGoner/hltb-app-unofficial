using MetroLog.Maui;

namespace HowLongToBeat.App;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        LogController.InitializeNavigation(
            page => Windows[0].Page!.Navigation.PushModalAsync(page),
            () => Windows[0].Page!.Navigation.PopModalAsync(),
            () => new MetroLogLocalizedPage());

        return new Window(new AppShell());
    }
}