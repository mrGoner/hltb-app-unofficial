using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using HowLongToBeat.App.Models;
using HowLongToBeat.Parser;
using HowLongToBeat.Parser.Models.Requests;

namespace HowLongToBeat.App.ViewModels;

public sealed class MainViewModel : INotifyPropertyChanged, IDisposable
{
    private readonly Func<GamesFilter, Task<GamesFilter?>> _showFilterPageFunc;
    private readonly Func<DisplayAlertParams, Task<bool>> _displayAlertWithCaptionFunc;

    private static readonly DisplayAlertParams FilterChangedAlertParams = new("",
        "Filter was changed, do you want to search again you current query?", "Yes", "No");
   
    private const string UserTokenName = "UserToken";
    private readonly HltbParser _hltbParser = new();
    private string _searchGameText = string.Empty;
    private GamesFilter _gameFilter = GamesFilter.Default;
    private bool _isLoading;
    private bool _isFilterPageOpened;
    private bool _isFilterModified;
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsFilterModified
    {
        get => _isFilterModified;
        set => SetField(ref _isFilterModified, value);
    }

    public string SearchGameText
    {
        get => _searchGameText;
        set => SetField(ref _searchGameText, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetField(ref _isLoading, value);
    }

    public ICommand SearchGameCommand =>
        new Command(() => _ = SearchGames(), () =>
        {
            if (IsLoading)
                Toast.Make("Already searching. Wait for complete").Show();

            return !string.IsNullOrEmpty(SearchGameText) && !IsLoading;
        });


    public ICommand ShowFilterCommand => new Command(() =>
    {
        if (IsLoading)
            Toast.Make("Searching in progress. Wait for complete").Show();
        
        if(_isFilterPageOpened)
            return;
        
        _isFilterPageOpened = true;

        _ = OpenFilterPageAndGetResult().ContinueWith(_ => _isFilterPageOpened = false);
    });

    public ObservableCollection<Game> Games { get; private set; } = [];

    public MainViewModel(Func<GamesFilter, Task<GamesFilter?>> showFilterPageFunc,
        Func<DisplayAlertParams, Task<bool>> displayAlertWithCaptionFunc)
    {
        _showFilterPageFunc = showFilterPageFunc;
        _displayAlertWithCaptionFunc = displayAlertWithCaptionFunc;
    }

    private async Task SearchGames()
    {
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(20));

        try
        {
            IsLoading = true;

            Games.Clear();

            var searchOptions = SearchOptions.Default with {Games = _gameFilter};
            var searchTerms = _searchGameText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var searchRequest = new SearchRequest(SearchType.Games, searchTerms, 1, 20, searchOptions, true);
            
            var storedUserToken = Preferences.Get(UserTokenName, null);

            if (storedUserToken == null)
            {
                storedUserToken = await _hltbParser.TryGetUserToken(cancellationTokenSource.Token);

                if (storedUserToken == null)
                    throw new Exception("Failed to get user token");

                Preferences.Set(UserTokenName, storedUserToken);
            }

            var response = await _hltbParser.Search(searchRequest, new Context(storedUserToken),
                cancellationTokenSource.Token);

            Games = new ObservableCollection<Game>(response.Games.Select(game =>
                new Game(game.Name, game.ImageUrl, game.MainTime, game.PlusExtrasTime, game.PerfectTime, game.ReleaseWorld)));

            if (Games.Count == 0)
                await Toast.Make("No games found").Show();

            OnPropertyChanged(nameof(Games));
        }
        catch (Exception)
        {
            if (!cancellationTokenSource.IsCancellationRequested)
                Preferences.Remove(UserTokenName);
            
            await Toast.Make("Load failed, try again", ToastDuration.Long).Show();
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task OpenFilterPageAndGetResult()
    {
        var newFilter = await _showFilterPageFunc(_gameFilter);
        
        if(newFilter == null)
            return;

        var isFilterChanged = !_gameFilter.Equals(newFilter);

        _gameFilter = newFilter;

        IsFilterModified = !_gameFilter.Equals(GamesFilter.Default);

        if (isFilterChanged && !string.IsNullOrEmpty(SearchGameText))
        {
            var result = await _displayAlertWithCaptionFunc(FilterChangedAlertParams);

            if (result)
                _ = SearchGames();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) 
            return false;
        field = value;
        
        OnPropertyChanged(propertyName);
        return true;
    }

    public void Dispose()
    {
        _hltbParser.Dispose();
    }
}