namespace TaskManager.Api.Models;

public class TaskDependency
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public int TaskId { get; set; }
    public int DependsOnTaskId { get; set; }

    public TaskItem? Task { get; set; }
    public TaskItem? DependsOnTask { get; set; }
}
