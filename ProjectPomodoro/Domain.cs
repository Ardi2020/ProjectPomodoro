using System.Text.Json;
using System.IO;

namespace ProjectPomodoro;

public enum WorkStatus { Todo, InProgress, Done }
public enum SessionResult { Completed, StoppedEarly }
public sealed class ProjectData { public List<Project> Projects { get; set; } = []; public List<PomodoroSession> Sessions { get; set; } = []; public SettingsData Settings { get; set; } = new(); }
public sealed class SettingsData { public List<string> FocusMessages { get; set; } = ["One concrete step at a time."]; public List<string> CancelMessages { get; set; } = ["Remember why this matters."]; public bool CompletionSound { get; set; } = true; }
public sealed class Project
{
    public Guid Id { get; set; } = Guid.NewGuid(); public string Title { get; set; } = ""; public bool IsCompleted { get; set; } public bool IsArchived { get; set; }
    public List<Milestone> Milestones { get; set; } = []; public List<WorkItem> Tasks { get; set; } = [];
    public int LeafCount => Tasks.Sum(t => t.Subtasks.Count == 0 ? 1 : t.Subtasks.Count);
    public int DoneLeafCount => Tasks.Sum(t => t.Subtasks.Count == 0 ? (t.Status == WorkStatus.Done ? 1 : 0) : t.Subtasks.Count(s => s.Status == WorkStatus.Done));
    public string ProgressText => LeafCount == 0 ? "Not started" : $"{DoneLeafCount}/{LeafCount} leaves ({DoneLeafCount * 100 / LeafCount}%)";
}
public sealed class Milestone { public Guid Id { get; set; } = Guid.NewGuid(); public string Title { get; set; } = ""; public bool IsCompleted { get; set; } }
public sealed class WorkItem { public Guid Id { get; set; } = Guid.NewGuid(); public string Title { get; set; } = ""; public string Why { get; set; } = ""; public WorkStatus Status { get; set; } public Guid? MilestoneId { get; set; } public List<WorkItem> Subtasks { get; set; } = []; }
public sealed class PomodoroSession { public Guid Id { get; set; } = Guid.NewGuid(); public Guid WorkItemId { get; set; } public Guid ProjectId { get; set; } public DateTimeOffset StartedAt { get; set; } public DateTimeOffset EndedAt { get; set; } public int PlannedMinutes { get; set; } = 25; public int ActualFocusedSeconds { get; set; } public SessionResult Result { get; set; } public bool MarkedDoneAfter { get; set; } }
public sealed class LocalStore
{
    private readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProjectPomodoro", "data.json");
    private readonly JsonSerializerOptions options = new() { WriteIndented = true }; public ProjectData Data { get; private set; } = new();
    public void Load() { try { if (File.Exists(filePath)) Data = JsonSerializer.Deserialize<ProjectData>(File.ReadAllText(filePath), options) ?? new(); } catch { Data = new(); } }
    public void Save() { Directory.CreateDirectory(Path.GetDirectoryName(filePath)!); var temp = filePath + ".tmp"; File.WriteAllText(temp, JsonSerializer.Serialize(Data, options)); File.Move(temp, filePath, true); }
}