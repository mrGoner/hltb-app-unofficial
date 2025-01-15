using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using HowLongToBeat.Parser.JsonConverters;

namespace HowLongToBeat.Parser.Models.Requests;

public record GamesFilter(
    [property: JsonPropertyName("userId")]
    int UserId,
    [property: JsonPropertyName("platform")]
    GamesFilter.PlatformType Platform,
    [property: JsonPropertyName("sortCategory")]
    GamesFilter.SortCategoryType SortCategory,
    [property: JsonPropertyName("rangeCategory")]
    GamesFilter.RangeCategoryType RangeCategory,
    [property: JsonPropertyName("gameplay")]
    GamesFilter.Gameplay GameplayFilter,
    [property: JsonPropertyName("rangeTime")]
    GamesFilter.RangeTime RangeTimeFilter,
    [property: JsonPropertyName("rangeYear")]
    GamesFilter.RangeYear RangeYearFilter,
    [property: JsonPropertyName("modifier")]
    string Modifier)
{
    public static GamesFilter Default => new(0, PlatformType.All, SortCategoryType.Popular, RangeCategoryType.Main,
        Gameplay.Default, RangeTime.Default, RangeYear.Default, string.Empty);
    
    public record RangeTime(
        [property: JsonPropertyName("min")]
        [property: JsonConverter(typeof(IntToStringConverter))]
        int? Min,
        [property: JsonPropertyName("max")]
        [property: JsonConverter(typeof(IntToStringConverter))]
        int? Max)
    {
        public static RangeTime Default => new(null, null);
    }

    public record Gameplay(
        [property: JsonPropertyName("perspective")] 
        PerspectiveType Perspective,
        [property: JsonPropertyName("flow")] 
        FlowType Flow,
        [property: JsonPropertyName("genre")] 
        GenreType Genre,
        [property: JsonPropertyName("difficulty")]
        string Difficulty)
    {
        public static Gameplay Default => new(PerspectiveType.All, FlowType.All, GenreType.All, string.Empty);
    }

    public record RangeYear(
        [property: JsonPropertyName("min")] 
        [property: JsonConverter(typeof(IntToStringConverter))]
        int? Min, 
        [property: JsonPropertyName("max")]
        [property: JsonConverter(typeof(IntToStringConverter))]
        int? Max)
    {
        public static RangeYear Default => new(null, null);
    }
    
    public enum SortCategoryType
    {
        [EnumMember(Value = "popular")]
        Popular,
        [EnumMember(Value = "name")]
        Name,
        [EnumMember(Value = "rating")]
        TopRated,
        [EnumMember(Value = "release")]
        ReleaseDate,
    }

    public enum RangeCategoryType
    {
        [EnumMember(Value = "main")]
        Main,
        [EnumMember(Value = "comp")]
        Completionist,
        [EnumMember(Value = "mainp")]
        MainPlusExtras,
        [EnumMember(Value = "averagea")]
        AverageTime
    }
    
    public enum GenreType
    {
        [EnumMember(Value = "")]
        All,
        [EnumMember(Value = "Action")]
        Action,
        [EnumMember(Value = "Adventure")]
        Adventure,
        [EnumMember(Value = "Arcade")]
        Arcade,
        [EnumMember(Value = "Battle Arena")]
        BattleArena,
        [EnumMember(Value = "Beat em Up")]
        BeatEmUp,
        [EnumMember(Value = "Board Game")]
        BoardGame,
        [EnumMember(Value = "Breakout")]
        Breakout,
        [EnumMember(Value = "Card Game")]
        CardGame,
        [EnumMember(Value = "City-Building")]
        CityBuilding,
        [EnumMember(Value = "Compilation")]
        Compilation,
        [EnumMember(Value = "Educational")]
        Educational,
        [EnumMember(Value = "Fighting")]
        Fighting,
        [EnumMember(Value = "Fitness")]
        Fitness,
        [EnumMember(Value = "Flight")]
        Flight,
        [EnumMember(Value = "Full Motion Video (FMV)")]
        FullMotionVideo,
        [EnumMember(Value = "Hack and Slash")]
        HackAndSlash,
        [EnumMember(Value = "Hidden Object")]
        HiddenObject,
        [EnumMember(Value = "Horror")]
        Horror,
        [EnumMember(Value = "Interactive Art")]
        InteractiveArt,
        [EnumMember(Value = "Management")]
        Management,
        [EnumMember(Value = "Music/Rhythm")]
        MusicRhythm,
        [EnumMember(Value = "Open World")]
        OpenWorld,
        [EnumMember(Value = "Party")]
        Party,
        [EnumMember(Value = "Pinball")]
        Pinball,
        [EnumMember(Value = "Platform")]
        Platform,
        [EnumMember(Value = "Puzzle")]
        Puzzle,
        [EnumMember(Value = "Racing/Driving")]
        RacingDriving,
        [EnumMember(Value = "Roguelike")]
        Roguelike,
        [EnumMember(Value = "Role-Playing")]
        RolePlaying,
        [EnumMember(Value = "Sandbox")]
        Sandbox,
        [EnumMember(Value = "Shooter")]
        Shooter,
        [EnumMember(Value = "Simulation")]
        Simulation,
        [EnumMember(Value = "Social")]
        Social,
        [EnumMember(Value = "Sports")]
        Sports,
        [EnumMember(Value = "Stealth")]
        Stealth,
        [EnumMember(Value = "Strategy/Tactical")]
        StrategyTactical,
        [EnumMember(Value = "Survival")]
        Survival,
        [EnumMember(Value = "Tower Defense")]
        TowerDefense,
        [EnumMember(Value = "Trivia")]
        Trivia,
        [EnumMember(Value = "Vehicular Combat")]
        VehicularCombat,
        [EnumMember(Value = "Visual Novel")]
        VisualNovel
    }
    
    public enum PerspectiveType
    {
        [EnumMember(Value = "")]
        All,
        [EnumMember(Value = "First-Person")]
        FirstPerson,
        [EnumMember(Value = "Isometric")]
        Isometric,
        [EnumMember(Value = "Side")]
        Side,
        [EnumMember(Value = "Text")]
        Text,
        [EnumMember(Value = "Third-Person")]
        ThirdPerson,
        [EnumMember(Value = "Top-Down")]
        TopDown,
        [EnumMember(Value = "Virtual Reality")]
        VirtualReality
    }
    
    public enum FlowType
    {
        [EnumMember(Value = "")]
        All,
        [EnumMember(Value = "Incremental")]
        Incremental,
        [EnumMember(Value = "Massively Multiplayer")]
        MassivelyMultiplayer,
        [EnumMember(Value = "Multidirectional")]
        Multidirectional,
        [EnumMember(Value = "On-Rails")]
        OnRails,
        [EnumMember(Value = "Point-and-Click")]
        PointAndClick,
        [EnumMember(Value = "Real-Time")]
        RealTime,
        [EnumMember(Value = "Scrolling")]
        Scrolling,
        [EnumMember(Value = "Turn-Based")]
        TurnBased
    }
    
    public enum PlatformType
    {
        [EnumMember(Value = "")]
        All,
        [EnumMember(Value = "Emulated")]
        Emulated,
        [EnumMember(Value = "Linux")]
        Linux,
        [EnumMember(Value = "Mac")]
        Mac,
        [EnumMember(Value = "Nintendo 3DS")]
        Nintendo3DS,
        [EnumMember(Value = "Nintendo Switch")]
        NintendoSwitch,
        [EnumMember(Value = "PC")]
        PC,
        [EnumMember(Value = "PlayStation 3")]
        PlayStation3,
        [EnumMember(Value = "PlayStation 4")]
        PlayStation4,
        [EnumMember(Value = "PlayStation 5")]
        PlayStation5,
        [EnumMember(Value = "PlayStation Vita")]
        PlayStationVita,
        [EnumMember(Value = "Wii U")]
        WiiU,
        [EnumMember(Value = "Xbox 360")]
        Xbox360,
        [EnumMember(Value = "Xbox One")]
        XboxOne,
        [EnumMember(Value = "Xbox Series X/S")]
        XboxSeriesXS
    }
}