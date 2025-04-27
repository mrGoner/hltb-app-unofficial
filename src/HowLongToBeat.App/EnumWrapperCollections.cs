using HowLongToBeat.App.Models;
using HowLongToBeat.App.Resources;
using HowLongToBeat.Parser.Models.Requests;

namespace HowLongToBeat.App;

public static class EnumWrapperCollections
{
    public static readonly EnumValueHolder<GamesFilter.GenreType>[] GameGenreValues =
    [
        new(GamesFilter.GenreType.All, AppResources.GenreType_All),
        new(GamesFilter.GenreType.Action, AppResources.GenreType_Action),
        new(GamesFilter.GenreType.Adventure, AppResources.GenreType_Adventure),
        new(GamesFilter.GenreType.Arcade, AppResources.GenreType_Arcade),
        new(GamesFilter.GenreType.BattleArena, AppResources.GenreType_BattleArena),
        new(GamesFilter.GenreType.BeatEmUp, AppResources.GenreType_BeatEmUp),
        new(GamesFilter.GenreType.BoardGame, AppResources.GenreType_BoardGame),
        new(GamesFilter.GenreType.Breakout, AppResources.GenreType_Breakout),
        new(GamesFilter.GenreType.CardGame, AppResources.GenreType_CardGame),
        new(GamesFilter.GenreType.CityBuilding, AppResources.GenreType_CityBuilding),
        new(GamesFilter.GenreType.Compilation, AppResources.GenreType_Compilation),
        new(GamesFilter.GenreType.Educational, AppResources.GenreType_Educational),
        new(GamesFilter.GenreType.Fighting, AppResources.GenreType_Fighting),
        new(GamesFilter.GenreType.Fitness, AppResources.GenreType_Fitness),
        new(GamesFilter.GenreType.Flight, AppResources.GenreType_Flight),
        new(GamesFilter.GenreType.FullMotionVideo, AppResources.GenreType_FullMotionVideo),
        new(GamesFilter.GenreType.HackAndSlash, AppResources.GenreType_HackAndSlash),
        new(GamesFilter.GenreType.HiddenObject, AppResources.GenreType_HiddenObject),
        new(GamesFilter.GenreType.Horror, AppResources.GenreType_Horror),
        new(GamesFilter.GenreType.InteractiveArt, AppResources.GenreType_InteractiveArt),
        new(GamesFilter.GenreType.Management, AppResources.GenreType_Management),
        new(GamesFilter.GenreType.MusicRhythm, AppResources.GenreType_MusicRhythm),
        new(GamesFilter.GenreType.OpenWorld, AppResources.GenreType_OpenWorld),
        new(GamesFilter.GenreType.Party, AppResources.GenreType_Party),
        new(GamesFilter.GenreType.Pinball, AppResources.GenreType_Pinball),
        new(GamesFilter.GenreType.Platform, AppResources.GenreType_Platform),
        new(GamesFilter.GenreType.Puzzle, AppResources.GenreType_Puzzle),
        new(GamesFilter.GenreType.RacingDriving, AppResources.GenreType_RacingDriving),
        new(GamesFilter.GenreType.Roguelike, AppResources.GenreType_Roguelike),
        new(GamesFilter.GenreType.RolePlaying, AppResources.GenreType_RolePlaying),
        new(GamesFilter.GenreType.Sandbox, AppResources.GenreType_Sandbox),
        new(GamesFilter.GenreType.Shooter, AppResources.GenreType_Shooter),
        new(GamesFilter.GenreType.Simulation, AppResources.GenreType_Simulation),
        new(GamesFilter.GenreType.Social, AppResources.GenreType_Social),
        new(GamesFilter.GenreType.Sports, AppResources.GenreType_Sports),
        new(GamesFilter.GenreType.Stealth, AppResources.GenreType_Stealth),
        new(GamesFilter.GenreType.StrategyTactical, AppResources.GenreType_StrategyTactical),
        new(GamesFilter.GenreType.Survival, AppResources.GenreType_Survival),
        new(GamesFilter.GenreType.TowerDefense, AppResources.GenreType_TowerDefense),
        new(GamesFilter.GenreType.Trivia, AppResources.GenreType_Trivia),
        new(GamesFilter.GenreType.VehicularCombat, AppResources.GenreType_VehicularCombat),
        new(GamesFilter.GenreType.VisualNovel, AppResources.GenreType_VisualNovel)
    ];

    public static readonly EnumValueHolder<GamesFilter.PerspectiveType>[] PerspectiveValues =
    [
        new(GamesFilter.PerspectiveType.All, AppResources.PerspectiveType_All),
        new(GamesFilter.PerspectiveType.FirstPerson, AppResources.PerspectiveType_FirstPerson),
        new(GamesFilter.PerspectiveType.Isometric, AppResources.PerspectiveType_Isometric),
        new(GamesFilter.PerspectiveType.Side, AppResources.PerspectiveType_Side),
        new(GamesFilter.PerspectiveType.Text, AppResources.PerspectiveType_Text),
        new(GamesFilter.PerspectiveType.ThirdPerson, AppResources.PerspectiveType_ThirdPerson),
        new(GamesFilter.PerspectiveType.TopDown, AppResources.PerspectiveType_TopDown),
        new(GamesFilter.PerspectiveType.VirtualReality, AppResources.PerspectiveType_VirtualReality)
    ];

    public static readonly EnumValueHolder<GamesFilter.FlowType>[] GameFlowValues =
    [
        new(GamesFilter.FlowType.All, AppResources.FlowType_All),
        new(GamesFilter.FlowType.Incremental, AppResources.FlowType_Incremental),
        new(GamesFilter.FlowType.MassivelyMultiplayer, AppResources.FlowType_MassivelyMultiplayer),
        new(GamesFilter.FlowType.Multidirectional, AppResources.FlowType_Multidirectional),
        new(GamesFilter.FlowType.OnRails, AppResources.FlowType_OnRails),
        new(GamesFilter.FlowType.PointAndClick, AppResources.FlowType_PointAndClick),
        new(GamesFilter.FlowType.RealTime, AppResources.FlowType_RealTime),
        new(GamesFilter.FlowType.Scrolling, AppResources.FlowType_Scrolling),
        new(GamesFilter.FlowType.TurnBased, AppResources.FlowType_TurnBased)
    ];
        
    public static readonly EnumValueHolder<GamesFilter.PlatformType>[] PlatformValues =
    [
        new(GamesFilter.PlatformType.All, AppResources.PlatformType_All),
        new(GamesFilter.PlatformType.Emulated, AppResources.PlatformType_Emulated),
        new(GamesFilter.PlatformType.Nintendo3DS, AppResources.PlatformType_Nintendo3DS),
        new(GamesFilter.PlatformType.NintendoSwitch, AppResources.PlatformType_NintendoSwitch),
        new(GamesFilter.PlatformType.PC, AppResources.PlatformType_PC),
        new(GamesFilter.PlatformType.PlayStation3, AppResources.PlatformType_PlayStation3),
        new(GamesFilter.PlatformType.PlayStation4, AppResources.PlatformType_PlayStation4),
        new(GamesFilter.PlatformType.PlayStation5, AppResources.PlatformType_PlayStation5),
        new(GamesFilter.PlatformType.PlayStationVita, AppResources.PlatformType_PlayStationVita),
        new(GamesFilter.PlatformType.WiiU, AppResources.PlatformType_WiiU),
        new(GamesFilter.PlatformType.Xbox360, AppResources.PlatformType_Xbox360),
        new(GamesFilter.PlatformType.XboxOne, AppResources.PlatformType_XboxOne),
        new(GamesFilter.PlatformType.XboxSeriesXS, AppResources.PlatformType_XboxSeriesXS)
    ];

    public static readonly EnumValueHolder<GamesFilter.RangeCategoryType>[] RangeCategoryValues =
    [
        new(GamesFilter.RangeCategoryType.Main, AppResources.MainStory),
        new(GamesFilter.RangeCategoryType.MainPlusExtras, AppResources.MainPlusExtra),
        new(GamesFilter.RangeCategoryType.Completionist, AppResources.Completionist),
        new(GamesFilter.RangeCategoryType.AverageTime, AppResources.AverageTime),
    ];

    public static readonly EnumValueHolder<GamesFilter.SortCategoryType>[] SortCategoryValues =
    [
        new(GamesFilter.SortCategoryType.Popular, AppResources.SortCategoryType_Popular),
        new(GamesFilter.SortCategoryType.Name, AppResources.SortCategoryType_Name),
        new(GamesFilter.SortCategoryType.TopRated, AppResources.SortCategoryType_TopRated),
        new(GamesFilter.SortCategoryType.ReleaseDate, AppResources.SortCategoryType_ReleaseDate)
    ];
}