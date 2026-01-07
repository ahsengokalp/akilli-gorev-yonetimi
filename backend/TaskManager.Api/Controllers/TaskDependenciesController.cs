using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Controllers.DTOs;
using TaskManager.Api.Data;
using TaskManager.Api.Models;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]


public class TaskDependenciesController : ControllerBase
{
    private readonly AppDbContext _db;

    public TaskDependenciesController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> Create(CreateDependencyDto dto)
    {
        if (dto.UserId <= 0) return BadRequest("UserId is required.");
        if (dto.TaskId <= 0 || dto.DependsOnTaskId <= 0) return BadRequest("TaskId and DependsOnTaskId are required.");
        if (dto.TaskId == dto.DependsOnTaskId) return BadRequest("A task cannot depend on itself.");

        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == dto.TaskId && t.UserId == dto.UserId);
        var dependsOn = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == dto.DependsOnTaskId && t.UserId == dto.UserId);
        if (task is null || dependsOn is null) return NotFound("Task or dependency task not found for this user.");

        var exists = await _db.TaskDependencies.AnyAsync(d =>
            d.UserId == dto.UserId && d.TaskId == dto.TaskId && d.DependsOnTaskId == dto.DependsOnTaskId);
        if (exists) return Conflict("This dependency already exists.");

        if (await WouldCreateCycle(dto.UserId, dto.TaskId, dto.DependsOnTaskId))
            return BadRequest("Cyclic dependency detected (A → ... → A).");

        var dep = new TaskDependency
        {
            UserId = dto.UserId,
            TaskId = dto.TaskId,
            DependsOnTaskId = dto.DependsOnTaskId
        };

        _db.TaskDependencies.Add(dep);
        await _db.SaveChangesAsync();

        return Ok(new
        {
            dep.Id,
            dep.UserId,
            dep.TaskId,
            dep.DependsOnTaskId
        });

    }

    [HttpGet]
    public async Task<IActionResult> GetForTask([FromQuery] int userId, [FromQuery] int taskId)
    {
        var deps = await _db.TaskDependencies
            .Where(d => d.UserId == userId && d.TaskId == taskId)
            .ToListAsync();

        return Ok(deps);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dep = await _db.TaskDependencies.FindAsync(id);
        if (dep is null) return NotFound();

        _db.TaskDependencies.Remove(dep);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private async Task<bool> WouldCreateCycle(int userId, int taskId, int dependsOnTaskId)
    {
        var visited = new HashSet<int>();
        var stack = new Stack<int>();
        stack.Push(dependsOnTaskId);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (!visited.Add(current)) continue;
            if (current == taskId) return true;

            var nextIds = await _db.TaskDependencies
                .Where(d => d.UserId == userId && d.TaskId == current)
                .Select(d => d.DependsOnTaskId)
                .ToListAsync();

            foreach (var n in nextIds) stack.Push(n);
        }

        return false;
    }
}
