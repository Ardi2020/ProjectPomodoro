using System.Windows;
using System.Windows.Threading;

namespace ProjectPomodoro;

public partial class FocusWindow : Window
{
    private readonly DispatcherTimer timer = new() { Interval = TimeSpan.FromSeconds(1) };
    private readonly DateTimeOffset endsAt;
    private readonly Action stop;
    private readonly Action complete;
    public FocusWindow(WorkItem work, DateTimeOffset endsAt, string message, Action stop, Action complete)
    {
        InitializeComponent(); this.endsAt = endsAt; this.stop = stop; this.complete = complete;
        TitleText.Text = work.Title; WhyText.Text = work.Why; MotivationText.Text = message; Closed += (_, _) => { if (DateTimeOffset.UtcNow < this.endsAt) this.stop(); }; timer.Tick += Refresh; Refresh(null, EventArgs.Empty); timer.Start();
    }
    private void Refresh(object? sender, EventArgs e) { var remaining = endsAt - DateTimeOffset.UtcNow; if (remaining <= TimeSpan.Zero) { timer.Stop(); RemainingText.Text = "00:00"; complete(); Close(); return; } RemainingText.Text = $"{(int)remaining.TotalMinutes:00}:{remaining.Seconds:00}"; }
    private void Stop_Click(object sender, RoutedEventArgs e) { timer.Stop(); if (MessageBox.Show("Keep going?\n\nStop Anyway will preserve this as stopped early.", "Stop focus?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) { stop(); Close(); } else timer.Start(); }
}