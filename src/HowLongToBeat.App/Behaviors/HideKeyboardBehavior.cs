using CommunityToolkit.Maui.Core.Platform;

namespace HowLongToBeat.App.Behaviors;

public class HideKeyboardBehavior : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry entry)
    {
        entry.Completed += OnCompleted;
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.Completed -= OnCompleted;
        base.OnDetachingFrom(entry);
    }

    private void OnCompleted(object sender, EventArgs e)
    {
        if (sender is not Entry entry) 
            return;
        
        entry.Unfocus(); 
        entry.HideKeyboardAsync(CancellationToken.None);
    }
}