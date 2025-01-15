using System.Windows.Input;

namespace HowLongToBeat.App.ViewModels;

public sealed class AboutViewModel
{
    private const string GitHubLink = "https://github.com/mrGoner/hltb-app-unofficial";

    public ICommand OpenGitHubCommand => new Command(() => Launcher.OpenAsync(GitHubLink));
    public string AppVersion => AppInfo.VersionString;
}