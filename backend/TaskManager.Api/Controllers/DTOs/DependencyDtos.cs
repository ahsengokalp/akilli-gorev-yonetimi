namespace TaskManager.Api.Controllers.DTOs;

public record CreateDependencyDto(
    int UserId,
    int TaskId,
    int DependsOnTaskId
);
