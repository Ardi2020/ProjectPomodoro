using System.Windows;
using System.Windows.Threading;

namespace ProjectPomodoro;

public partial class BreakWindow : Window
{
    private readonly DispatcherTimer timer = new() { Interval = TimeSpan.FromSeconds(1) };
    private readonly DateTimeOffset endsAt;
    private readonly Action finish;
    public BreakWindow(string context, DateTimeOffset endsAt, Action finish)
    {
        InitializeComponent(); this.endsAt = endsAt; this.finish = finish; ContextText.Text = context; Closed += (_, _) => finish(); timer.Tick += Refresh; Refresh(null, EventArgs.Empty); timer.Start();
    }
    private void Refresh(object? sender, EventArgs e) { var remaining = endsAt - DateTimeOffset.UtcNow; if (remaining <= TimeSpan.Zero) { timer.Stop(); RemainingText.Text = "00:00"; finish(); Close(); return; } RemainingText.Text = $"{(int)remaining.TotalMinutes:00}:{remaining.Seconds:00}"; }
    private void Stop_Click(object sender, RoutedEventArgs e) { timer.Stop(); finish(); Close(); }
}