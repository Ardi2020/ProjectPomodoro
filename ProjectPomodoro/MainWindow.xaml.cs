using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ProjectPomodoro;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly LocalStore store = new();
    private readonly DispatcherTimer refreshTimer = new() { Interval = TimeSpan.FromSeconds(1) };
    private Project? selectedProject;
    private WorkItem? activeWork;
    private PomodoroSession? activeSession;
    private DateTimeOffset activeEnd;
    private FocusWindow? focusWindow;
    private BreakWindow? breakWindow;

    public MainWindow()
    {
        InitializeComponent();
        store.Load();
        refreshTimer.Tick += (_, _) => RefreshFocusWindow();
        RefreshProjects();
    }

    private void RefreshProjects() { ProjectList.ItemsSource = null; ProjectList.ItemsSource = store.Data.Projects.Where(p => !p.IsArchived).ToList(); if (selectedProject != null) ProjectList.SelectedItem = selectedProject; RenderProject(); }
    private void ProjectList_SelectionChanged(object sender, SelectionChangedEventArgs e) { selectedProject = ProjectList.SelectedItem as Project; RenderProject(); }
    private void RenderProject() { ProjectTitle.Text = selectedProject?.Title ?? "Select a project"; ProjectProgress.Text = selectedProject?.ProgressText ?? ""; WorkItemsPanel.Children.Clear(); if (selectedProject == null) return; foreach (var milestone in selectedProject.Milestones) WorkItemsPanel.Children.Add(new TextBlock { Text = $"MILESTONE  ·  {milestone.Title}", FontWeight = FontWeights.Bold, Foreground = System.Windows.Media.Brushes.Teal, Margin = new Thickness(0, 8, 0, 10) }); foreach (var item in selectedProject.Tasks) AddWorkCard(item, false); }
    private void AddWorkCard(WorkItem item, bool subtask)
    {
        var card = new Border { Background = subtask ? System.Windows.Media.Brushes.Transparent : System.Windows.Media.Brushes.WhiteSmoke, Padding = new Thickness(14), Margin = new Thickness(subtask ? 28 : 0, 0, 0, 10), BorderBrush = System.Windows.Media.Brushes.LightGray, BorderThickness = new Thickness(0, 0, 0, 1) };
        var row = new DockPanel(); var actions = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
        var focus = new Button { Content = "Start 25 min", Style = (Style)FindResource("ActionButton"), Tag = item }; focus.Click += StartFocus_Click; actions.Children.Add(focus);
        if (!subtask) { var child = new Button { Content = "+ Subtask", Style = (Style)FindResource("QuietButton"), Tag = item, Margin = new Thickness(8, 0, 0, 0) }; child.Click += AddSubtask_Click; actions.Children.Add(child); }
        var done = new Button { Content = item.Status == WorkStatus.Done ? "Reopen" : "Mark done", Style = (Style)FindResource("QuietButton"), Tag = item, Margin = new Thickness(8, 0, 0, 0) }; done.Click += ToggleDone_Click; actions.Children.Add(done); DockPanel.SetDock(actions, Dock.Right); row.Children.Add(actions);
        var sessions = store.Data.Sessions.Where(s => s.WorkItemId == item.Id).ToList(); var focusedMinutes = sessions.Sum(s => s.ActualFocusedSeconds) / 60;
        var text = new StackPanel(); text.Children.Add(new TextBlock { Text = item.Title, FontSize = 18, FontWeight = FontWeights.SemiBold, Foreground = System.Windows.Media.Brushes.DarkSlateGray }); text.Children.Add(new TextBlock { Text = $"{item.Status}{(string.IsNullOrWhiteSpace(item.Why) ? "" : " - " + item.Why)}", Foreground = System.Windows.Media.Brushes.Gray, Margin = new Thickness(0, 4, 0, 0), TextWrapping = TextWrapping.Wrap }); text.Children.Add(new TextBlock { Text = $"{sessions.Count} Pomodoro{(sessions.Count == 1 ? "" : "s")}  ·  {focusedMinutes} focused min", Foreground = System.Windows.Media.Brushes.Gray, Margin = new Thickness(0, 4, 0, 0) }); row.Children.Add(text); card.Child = row; WorkItemsPanel.Children.Add(card); foreach (var sub in item.Subtasks) AddWorkCard(sub, true);
    }
    private void NewProject_Click(object sender, RoutedEventArgs e) { var title = Prompt("New project title:", "Create project"); if (string.IsNullOrWhiteSpace(title)) return; var project = new Project { Title = title.Trim() }; store.Data.Projects.Add(project); store.Save(); selectedProject = project; RefreshProjects(); ProjectList.SelectedItem = project; }
    private void AddTask_Click(object sender, RoutedEventArgs e) { if (selectedProject == null) return; var title = Prompt("Task title:", "Add task"); if (string.IsNullOrWhiteSpace(title)) return; selectedProject.Tasks.Add(new WorkItem { Title = title.Trim() }); store.Save(); RenderProject(); }
    private void AddMilestone_Click(object sender, RoutedEventArgs e) { if (selectedProject == null) return; var title = Prompt("Milestone title:", "Add milestone"); if (string.IsNullOrWhiteSpace(title)) return; selectedProject.Milestones.Add(new Milestone { Title = title.Trim() }); store.Save(); RenderProject(); }
    private void History_Click(object sender, RoutedEventArgs e) { if (selectedProject == null) return; var sessions = store.Data.Sessions.Where(s => s.ProjectId == selectedProject.Id).OrderByDescending(s => s.EndedAt).ToList(); MessageBox.Show(sessions.Count == 0 ? "No completed Pomodoro sessions yet." : string.Join(Environment.NewLine, sessions.Select(s => $"{s.EndedAt.LocalDateTime:g}  {s.Result}  {s.ActualFocusedSeconds / 60} min")), "Pomodoro history"); }
    private void StartFocus_Click(object sender, RoutedEventArgs e) { if (activeSession != null || breakWindow != null) { MessageBox.Show("A Focus or Break timer is already active."); return; } activeWork = (WorkItem)((Button)sender).Tag; if (activeWork.Status == WorkStatus.Done) { MessageBox.Show("Reopen this work item before starting Pomodoro."); return; } activeWork.Status = WorkStatus.InProgress; activeSession = new PomodoroSession { WorkItemId = activeWork.Id, ProjectId = selectedProject!.Id, StartedAt = DateTimeOffset.UtcNow }; activeEnd = DateTimeOffset.UtcNow.AddMinutes(25); refreshTimer.Start(); store.Save(); RenderProject(); ShowFocusWindow(); }
    private void AddSubtask_Click(object sender, RoutedEventArgs e) { var parent = (WorkItem)((Button)sender).Tag; var title = Prompt("Subtask title:", "Add subtask"); if (string.IsNullOrWhiteSpace(title)) return; parent.Subtasks.Add(new WorkItem { Title = title.Trim() }); store.Save(); RenderProject(); }
    private void ShowFocusWindow() { focusWindow?.Close(); var message = store.Data.Settings.FocusMessages.FirstOrDefault() ?? "Focus on this next step."; focusWindow = new FocusWindow(activeWork!, activeEnd, message, () => FinishFocus(SessionResult.StoppedEarly), () => FinishFocus(SessionResult.Completed)); focusWindow.Show(); }
    private void RefreshFocusWindow() { if (activeSession != null && DateTimeOffset.UtcNow >= activeEnd) FinishFocus(SessionResult.Completed); }
    private void ToggleDone_Click(object sender, RoutedEventArgs e) { var item = (WorkItem)((Button)sender).Tag; item.Status = item.Status == WorkStatus.Done ? WorkStatus.InProgress : WorkStatus.Done; store.Save(); RenderProject(); }
    private void CompleteProject_Click(object sender, RoutedEventArgs e) { if (selectedProject == null) return; selectedProject.IsCompleted = true; store.Save(); RenderProject(); }
    private void ArchiveProject_Click(object sender, RoutedEventArgs e) { if (selectedProject == null) return; selectedProject.IsArchived = true; store.Save(); selectedProject = null; RefreshProjects(); }
    private void Settings_Click(object sender, RoutedEventArgs e) { var value = Prompt("Focus message (saved locally):", "Settings", store.Data.Settings.FocusMessages.FirstOrDefault() ?? ""); if (!string.IsNullOrWhiteSpace(value)) { store.Data.Settings.FocusMessages = [value.Trim()]; store.Save(); } }
    private void FinishFocus(SessionResult result) { if (activeSession == null || activeWork == null) return; activeSession.EndedAt = DateTimeOffset.UtcNow; activeSession.ActualFocusedSeconds = Math.Max(0, (int)(activeSession.EndedAt - activeSession.StartedAt).TotalSeconds); activeSession.Result = result; store.Data.Sessions.Add(activeSession); activeSession = null; refreshTimer.Stop(); store.Save(); RenderProject(); if (result == SessionResult.Completed) PostFocusChoice(); }
    private void PostFocusChoice() { var choice = MessageBox.Show($"Pomodoro completed for {activeWork!.Title}.\n\nYes = Mark done\nNo = Take a break\nCancel = Start another Pomodoro", "What next?", MessageBoxButton.YesNoCancel); if (choice == MessageBoxResult.Yes) { activeWork.Status = WorkStatus.Done; MarkLastSessionDone(); store.Save(); RenderProject(); } else if (choice == MessageBoxResult.No) StartBreak(); else if (choice == MessageBoxResult.Cancel) StartFocusForCurrent(); }
    private void MarkLastSessionDone() { var session = store.Data.Sessions.LastOrDefault(s => s.WorkItemId == activeWork!.Id); if (session != null) session.MarkedDoneAfter = true; }
    private void StartFocusForCurrent() { if (activeWork == null) return; activeSession = new PomodoroSession { WorkItemId = activeWork.Id, ProjectId = selectedProject!.Id, StartedAt = DateTimeOffset.UtcNow }; activeEnd = DateTimeOffset.UtcNow.AddMinutes(25); refreshTimer.Start(); ShowFocusWindow(); }
    private void StartBreak() { if (activeWork == null || breakWindow != null) return; var option = MessageBox.Show("Yes = 5 minutes\nNo = 10 minutes\nCancel = custom duration", "Choose break", MessageBoxButton.YesNoCancel); var minutes = option == MessageBoxResult.Yes ? 5 : option == MessageBoxResult.No ? 10 : int.TryParse(Prompt("Break duration in minutes:", "Custom break"), out var custom) && custom > 0 ? custom : 0; if (minutes == 0) return; var end = DateTimeOffset.UtcNow.AddMinutes(minutes); breakWindow = new BreakWindow(activeWork.Title, end, () => { breakWindow = null; }); breakWindow.Show(); }
    private static string? Prompt(string text, string caption, string value = "") { var dialog = new Window { Title = caption, Width = 420, Height = 160, WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = Application.Current.MainWindow }; var panel = new StackPanel { Margin = new Thickness(18) }; panel.Children.Add(new TextBlock { Text = text }); var input = new TextBox { Text = value, Margin = new Thickness(0, 10, 0, 10) }; panel.Children.Add(input); var ok = new Button { Content = "Save", IsDefault = true, Width = 80, HorizontalAlignment = HorizontalAlignment.Right }; ok.Click += (_, _) => dialog.DialogResult = true; panel.Children.Add(ok); dialog.Content = panel; return dialog.ShowDialog() == true ? input.Text : null; }
}