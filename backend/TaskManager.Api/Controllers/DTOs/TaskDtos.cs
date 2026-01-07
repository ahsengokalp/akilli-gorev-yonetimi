using TaskManager.Api.Models;

namespace TaskManager.Api.Controllers.DTOs;

public record CreateTaskDto(
    int UserId,
    string Title,
    string? Description,
    PriorityLevel Priority,
    int EstimatedMinutes,
    DateTime Deadline
);

public record UpdateTaskDto(
    string Title,
    string? Description,
    PriorityLevel Priority,
    int EstimatedMinutes,
    DateTime Deadline,
    Models.TaskStatus Status
);
