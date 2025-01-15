using HowLongToBeat.App.Models;
using HowLongToBeat.Parser.Models.Requests;

namespace HowLongToBeat.App.ViewModels;

public static class EnumWrapperCollections
{
    public static readonly EnumValueHolder<GamesFilter.GenreType>[] GameGenreValues =
    [
        new(GamesFilter.GenreType.All, "All"),
        new(GamesFilter.GenreType.Action, "Action"),
        new(GamesFilter.GenreType.Adventure, "Adventure"),
        new(GamesFilter.GenreType.Arcade, "Arcade"),
        new(GamesFilter.GenreType.BattleArena, "Battle Arena"),
        new(GamesFilter.GenreType.BeatEmUp, "Beat em Up"),
        new(GamesFilter.GenreType.BoardGame, "Board Game"),
        new(GamesFilter.GenreType.Breakout, "Breakout"),
        new(GamesFilter.GenreType.CardGame, "Card Game"),
        new(GamesFilter.GenreType.CityBuilding, "City-Building"),
        new(GamesFilter.GenreType.Compilation, "Compilation"),
        new(GamesFilter.GenreType.Educational, "Educational"),
        new(GamesFilter.GenreType.Fighting, "Fighting"),
        new(GamesFilter.GenreType.Fitness, "Fitness"),
        new(GamesFilter.GenreType.Flight, "Flight"),
        new(GamesFilter.GenreType.FullMotionVideo, "Full Motion Video (FMV)"),
        new(GamesFilter.GenreType.HackAndSlash, "Hack and Slash"),
        new(GamesFilter.GenreType.HiddenObject, "Hidden Object"),
        new(GamesFilter.GenreType.Horror, "Horror"),
        new(GamesFilter.GenreType.InteractiveArt, "Interactive Art"),
        new(GamesFilter.GenreType.Management, "Management"),
        new(GamesFilter.GenreType.MusicRhythm, "Music/Rhythm"),
        new(GamesFilter.GenreType.OpenWorld, "Open World"),
        new(GamesFilter.GenreType.Party, "Party"),
        new(GamesFilter.GenreType.Pinball, "Pinball"),
        new(GamesFilter.GenreType.Platform, "Platform"),
        new(GamesFilter.GenreType.Puzzle, "Puzzle"),
        new(GamesFilter.GenreType.RacingDriving, "Racing/Driving"),
        new(GamesFilter.GenreType.Roguelike, "Roguelike"),
        new(GamesFilter.GenreType.RolePlaying, "Role-Playing"),
        new(GamesFilter.GenreType.Sandbox, "Sandbox"),
        new(GamesFilter.GenreType.Shooter, "Shooter"),
        new(GamesFilter.GenreType.Simulation, "Simulation"),
        new(GamesFilter.GenreType.Social, "Social"),
        new(GamesFilter.GenreType.Sports, "Sports"),
        new(GamesFilter.GenreType.Stealth, "Stealth"),
        new(GamesFilter.GenreType.StrategyTactical, "Strategy/Tactical"),
        new(GamesFilter.GenreType.Survival, "Survival"),
        new(GamesFilter.GenreType.TowerDefense, "Tower Defense"),
        new(GamesFilter.GenreType.Trivia, "Trivia"),
        new(GamesFilter.GenreType.VehicularCombat, "Vehicular Combat"),
        new(GamesFilter.GenreType.VisualNovel, "Visual Novel")
    ];

    public static readonly EnumValueHolder<GamesFilter.PerspectiveType>[] PerspectiveValues =
    [
        new(GamesFilter.PerspectiveType.All, "All"),
        new(GamesFilter.PerspectiveType.FirstPerson, "First-Person"),
        new(GamesFilter.PerspectiveType.Isometric, "Isometric"),
        new(GamesFilter.PerspectiveType.Side, "Side"),
        new(GamesFilter.PerspectiveType.Text, "Text"),
        new(GamesFilter.PerspectiveType.ThirdPerson, "Third-Person"),
        new(GamesFilter.PerspectiveType.TopDown, "Top-Down"),
        new(GamesFilter.PerspectiveType.VirtualReality, "Virtual Reality")
    ];

    public static readonly EnumValueHolder<GamesFilter.FlowType>[] GameFlowValues =
    [
        new(GamesFilter.FlowType.All, "All"),
        new(GamesFilter.FlowType.Incremental, "Incremental"),
        new(GamesFilter.FlowType.MassivelyMultiplayer, "Massively Multiplayer"),
        new(GamesFilter.FlowType.Multidirectional, "Multidirectional"),
        new(GamesFilter.FlowType.OnRails, "On-Rails"),
        new(GamesFilter.FlowType.PointAndClick, "Point-and-Click"),
        new(GamesFilter.FlowType.RealTime, "Real-Time"),
        new(GamesFilter.FlowType.Scrolling, "Scrolling"),
        new(GamesFilter.FlowType.TurnBased, "Turn-Based")
    ];
        
    public static readonly EnumValueHolder<GamesFilter.PlatformType>[] PlatformValues =
    [
        new (GamesFilter.PlatformType.All, "All"),
        new (GamesFilter.PlatformType.Emulated, "Emulated"),
        new (GamesFilter.PlatformType.Nintendo3DS, "Nintendo 3DS"),
        new (GamesFilter.PlatformType.NintendoSwitch, "Nintendo Switch"),
        new (GamesFilter.PlatformType.PC, "PC"),
        new (GamesFilter.PlatformType.PlayStation3, "PlayStation 3"),
        new (GamesFilter.PlatformType.PlayStation4, "PlayStation 4"),
        new (GamesFilter.PlatformType.PlayStation5, "PlayStation 5"),
        new (GamesFilter.PlatformType.PlayStationVita, "PlayStation Vita"),
        new (GamesFilter.PlatformType.WiiU, "Wii U"),
        new (GamesFilter.PlatformType.Xbox360, "Xbox 360"),
        new (GamesFilter.PlatformType.XboxOne, "Xbox One"),
        new (GamesFilter.PlatformType.XboxSeriesXS, "Xbox Series X/S")
    ];

    public static readonly EnumValueHolder<GamesFilter.RangeCategoryType>[] RangeCategoryValues =
    [
        new(GamesFilter.RangeCategoryType.Main, "Main Story"),
        new(GamesFilter.RangeCategoryType.MainPlusExtras, "Main + Extras"),
        new(GamesFilter.RangeCategoryType.Completionist, "Completionist"),
        new(GamesFilter.RangeCategoryType.AverageTime, "Average Time"),
    ];

    public static readonly EnumValueHolder<GamesFilter.SortCategoryType>[] SortCategoryValues =
    [
        new(GamesFilter.SortCategoryType.Popular, "Most Popular"),
        new(GamesFilter.SortCategoryType.Name, "Name"),
        new(GamesFilter.SortCategoryType.TopRated, "Top Rated"),
        new(GamesFilter.SortCategoryType.ReleaseDate, "Release Date")
    ];
}