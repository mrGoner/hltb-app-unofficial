namespace HowLongToBeat.App.Behaviors;

using Microsoft.Maui.Controls;

public sealed class NullableIntBehavior : Behavior<Entry>
{
    public int MaxLength { get; set; } = int.MaxValue;

    protected override void OnAttachedTo(Entry bindable)
    {
        base.OnAttachedTo(bindable);
        bindable.TextChanged += OnTextChanged;
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        base.OnDetachingFrom(bindable);
        bindable.TextChanged -= OnTextChanged;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            var newText = string.Concat(entry.Text?.Where(char.IsDigit) ?? string.Empty);

            if (newText.Length > MaxLength)
            {
                newText = newText.Substring(0, MaxLength);
            }

            if (entry.Text != newText)
            {
                entry.Text = (string.IsNullOrWhiteSpace(newText) ? null : newText)!;
                return;
            }

            entry.SetValue(Entry.TextProperty, string.IsNullOrEmpty(newText) ? null : int.Parse(newText));
        }
    }
    
}
