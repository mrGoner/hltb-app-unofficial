using CommunityToolkit.Maui.Views;
using HowLongToBeat.App.ViewModels;
using HowLongToBeat.Parser.Models.Requests;

namespace HowLongToBeat.App;

public partial class FilterPage : Popup
{
    public FilterPage(GamesFilter filter)
    {
        InitializeComponent();
        BindingContext = new FilterViewModel(filter, Close);
        ResultWhenUserTapsOutsideOfPopup = null;
    }
}