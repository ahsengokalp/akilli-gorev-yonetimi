namespace TaskManager.Api.Models;

public enum PriorityLevel { Low = 0, Medium = 1, High = 2 }
public enum TaskStatus { Todo = 0, InProgress = 1, Done = 2 }

public class TaskItem
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public PriorityLevel Priority { get; set; }
    public int EstimatedMinutes { get; set; }
    public DateTime Deadline { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Todo;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
    public List<TaskDependency> Dependencies { get; set; } = new();
}
