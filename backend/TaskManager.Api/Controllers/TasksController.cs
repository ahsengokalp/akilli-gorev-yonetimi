using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Controllers.DTOs;
using TaskManager.Api.Data;
using TaskManager.Api.Models;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;

    public TasksController(AppDbContext db) => _db = db;

    // GET /api/Tasks?userId=1
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int userId)
    {
        var tasks = await _db.Tasks
            .Where(t => t.UserId == userId)
            .OrderBy(t => t.Deadline)
            .ToListAsync();

        return Ok(tasks);
    }

    // POST /api/Tasks
    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        if (dto.UserId <= 0) return BadRequest("UserId is required.");
        if (string.IsNullOrWhiteSpace(dto.Title)) return BadRequest("Title is required.");
        if (dto.EstimatedMinutes <= 0) return BadRequest("EstimatedMinutes must be > 0.");

        var task = new TaskItem
        {
            UserId = dto.UserId,
            Title = dto.Title.Trim(),
            Description = dto.Description,
            Priority = dto.Priority,
            EstimatedMinutes = dto.EstimatedMinutes,
            Deadline = dto.Deadline,
            Status = Models.TaskStatus.Todo
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { userId = dto.UserId }, task);
    }

    // PUT /api/Tasks/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        if (string.IsNullOrWhiteSpace(dto.Title)) return BadRequest("Title is required.");
        if (dto.EstimatedMinutes <= 0) return BadRequest("EstimatedMinutes must be > 0.");

        task.Title = dto.Title.Trim();
        task.Description = dto.Description;
        task.Priority = dto.Priority;
        task.EstimatedMinutes = dto.EstimatedMinutes;
        task.Deadline = dto.Deadline;
        task.Status = dto.Status;

        await _db.SaveChangesAsync();
        return Ok(task);
    }

    // DELETE /api/Tasks/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        var isBlocking = await _db.TaskDependencies.AnyAsync(d => d.DependsOnTaskId == id);
        if (isBlocking) return Conflict("This task is a dependency for another task. Remove dependencies first.");

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
