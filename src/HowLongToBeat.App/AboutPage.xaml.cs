using CommunityToolkit.Maui.Views;
using HowLongToBeat.App.ViewModels;

namespace HowLongToBeat.App;

public partial class AboutPage : Popup
{
    public AboutPage()
    {
        InitializeComponent();
        BindingContext = new AboutViewModel();
    }
}