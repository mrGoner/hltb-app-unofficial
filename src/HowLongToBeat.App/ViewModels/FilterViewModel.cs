using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HowLongToBeat.App.Models;
using HowLongToBeat.Parser.Models.Requests;

namespace HowLongToBeat.App.ViewModels;

public sealed class FilterViewModel : INotifyPropertyChanged
{
    private readonly Func<GamesFilter?, CancellationToken, Task> _closeAndSetResultAction;
    private EnumValueHolder<GamesFilter.PerspectiveType> _selectedPerspective;
    private EnumValueHolder<GamesFilter.FlowType> _selectedFlow;
    private EnumValueHolder<GamesFilter.GenreType> _selectedGenre;
    private int? _releaseYearMin;
    private EnumValueHolder<GamesFilter.PlatformType> _selectedPlatform;
    private EnumValueHolder<GamesFilter.RangeCategoryType> _selectedCategoryRange;
    private EnumValueHolder<GamesFilter.SortCategoryType> _selectedSortCategory;
    private int? _releaseYearMax;
    private int? _hourRangeMin;
    private int? _hourRangeMax;
    public EnumValueHolder<GamesFilter.PerspectiveType>[] Perspectives => EnumWrapperCollections.PerspectiveValues;
    public EnumValueHolder<GamesFilter.FlowType>[] Flows => EnumWrapperCollections.GameFlowValues;
    public EnumValueHolder<GamesFilter.GenreType>[] Genres => EnumWrapperCollections.GameGenreValues;
    public EnumValueHolder<GamesFilter.PlatformType>[] Platforms => EnumWrapperCollections.PlatformValues;
    public EnumValueHolder<GamesFilter.RangeCategoryType>[] CategoryRanges => EnumWrapperCollections.RangeCategoryValues;
    public EnumValueHolder<GamesFilter.SortCategoryType>[] SortCategories => EnumWrapperCollections.SortCategoryValues;

    public EnumValueHolder<GamesFilter.PerspectiveType> SelectedPerspective
    {
        get => _selectedPerspective;
        set => SetField(ref _selectedPerspective, value);
    }

    public EnumValueHolder<GamesFilter.FlowType> SelectedFlow
    {
        get => _selectedFlow;
        set => SetField(ref _selectedFlow, value);
    }

    public EnumValueHolder<GamesFilter.GenreType> SelectedGenre
    {
        get => _selectedGenre;
        set => SetField(ref _selectedGenre, value);
    }

    public EnumValueHolder<GamesFilter.PlatformType> SelectedPlatform
    {
        get => _selectedPlatform;
        set => SetField(ref _selectedPlatform, value);
    }

    public EnumValueHolder<GamesFilter.RangeCategoryType> SelectedCategoryRange
    {
        get => _selectedCategoryRange;
        set => SetField(ref _selectedCategoryRange, value);
    }

    public EnumValueHolder<GamesFilter.SortCategoryType> SelectedSortCategory
    {
        get => _selectedSortCategory;
        set => SetField(ref _selectedSortCategory, value);
    }

    public int? ReleaseYearMin
    {
        get => _releaseYearMin;
        set => SetField(ref _releaseYearMin, value);
    }

    public int? ReleaseYearMax
    {
        get => _releaseYearMax;
        set => SetField(ref _releaseYearMax, value);
    }

    public int? HourRangeMin
    {
        get => _hourRangeMin;
        set => SetField(ref _hourRangeMin, value);
    }

    public int? HourRangeMax
    {
        get => _hourRangeMax;
        set => SetField(ref _hourRangeMax, value);
    }

    public ICommand ResetCommand => new Command(ResetFilter);

    public ICommand SaveCommand =>
        new Command(async () => await _closeAndSetResultAction(BuildFilter(), CancellationToken.None));
    

    public event PropertyChangedEventHandler? PropertyChanged;

    public FilterViewModel(GamesFilter filter, Func<GamesFilter?, CancellationToken, Task> closeAndSetResultAction)
    {
        _closeAndSetResultAction = closeAndSetResultAction;
        ApplyFilter(filter);
    }

    private void ApplyFilter(GamesFilter filter)
    {
        SelectedPerspective =
            Perspectives.FirstOrDefault(holder => holder.Value == filter.GameplayFilter.Perspective) ??
            Perspectives.First();

        SelectedFlow = Flows.FirstOrDefault(holder => holder.Value == filter.GameplayFilter.Flow) ??
                       Flows.First();

        SelectedGenre = Genres.FirstOrDefault(holder => holder.Value == filter.GameplayFilter.Genre) ??
                        Genres.First();

        SelectedPlatform = Platforms.FirstOrDefault(holder => holder.Value == filter.Platform) ??
                           Platforms.First();

        SelectedCategoryRange = CategoryRanges.FirstOrDefault(holder => holder.Value == filter.RangeCategory) ??
                                CategoryRanges.First();

        SelectedSortCategory = SortCategories.FirstOrDefault(holder => holder.Value == filter.SortCategory) ??
                               SortCategories.First();

        ReleaseYearMin = filter.RangeYearFilter.Min;
        ReleaseYearMax = filter.RangeYearFilter.Max;

        HourRangeMin = filter.RangeTimeFilter.Min;
        HourRangeMax = filter.RangeTimeFilter.Max;
    }

    private GamesFilter BuildFilter()
    {
        var gameplay = new GamesFilter.Gameplay(SelectedPerspective.Value, SelectedFlow.Value,
            SelectedGenre.Value, string.Empty);

        var rangeTime = new GamesFilter.RangeTime(HourRangeMin, HourRangeMax);
        var rangeYear = new GamesFilter.RangeYear(ReleaseYearMin, ReleaseYearMax);

        return new GamesFilter(0, SelectedPlatform.Value, SelectedSortCategory.Value, SelectedCategoryRange.Value,
            gameplay, rangeTime, rangeYear, string.Empty);
    }
    
    private void ResetFilter()
    {
        SelectedPerspective = EnumWrapperCollections.PerspectiveValues.First();
        SelectedFlow = EnumWrapperCollections.GameFlowValues.First();
        SelectedGenre = EnumWrapperCollections.GameGenreValues.First();
        SelectedPlatform = EnumWrapperCollections.PlatformValues.First();
        SelectedCategoryRange = EnumWrapperCollections.RangeCategoryValues.First();
        SelectedSortCategory = EnumWrapperCollections.SortCategoryValues.First();

        ReleaseYearMin = null;
        ReleaseYearMax = null;
        
        HourRangeMin = null;
        HourRangeMax = null;
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
}