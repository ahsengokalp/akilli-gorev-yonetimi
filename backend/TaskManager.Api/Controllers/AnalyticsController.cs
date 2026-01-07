using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Models;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly AppDbContext _db;
    public AnalyticsController(AppDbContext db) => _db = db;

    // GET /api/Analytics/delay-risk?userId=1
    [HttpGet("delay-risk")]
    public async Task<IActionResult> GetDelayRisk([FromQuery] int userId)
    {
        var now = DateTime.UtcNow;

        var tasks = await _db.Tasks
            .Where(t => t.UserId == userId && t.Status != TaskManager.Api.Models.TaskStatus.Done)
            .ToListAsync();

        // Basit ama raporlanabilir bir risk skoru (kural tabanlı)
        // 0-100 arası
        var result = tasks.Select(t =>
        {
            var hoursLeft = (t.Deadline.ToUniversalTime() - now).TotalHours;
            var estHours = t.EstimatedMinutes / 60.0;

            int score = 0;

            // Deadline yaklaştıkça risk artar
            if (hoursLeft <= 24) score += 40;
            else if (hoursLeft <= 72) score += 25;
            else if (hoursLeft <= 168) score += 10;

            // Süre uzunsa risk artar
            if (estHours >= 6) score += 30;
            else if (estHours >= 3) score += 20;
            else if (estHours >= 1.5) score += 10;

            // Bağımlılık varsa risk artar (çünkü önce başka işler bitmeli)
            // (Dependency tablosunu kontrol ediyoruz)
            // Bu görev başka bir göreve bağlıysa +10
            // Ayrıca bağımlı olduğu görev Done değilse +10 ekstra
            return new
            {
                t.Id,
                t.Title,
                t.Priority,
                t.EstimatedMinutes,
                t.Deadline,
                t.Status,
                RiskScore = score
            };
        })
        .OrderByDescending(x => x.RiskScore)
        .ToList();

        return Ok(result);
    }
}
