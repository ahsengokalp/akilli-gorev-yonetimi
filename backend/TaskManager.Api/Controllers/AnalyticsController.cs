using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Models;
using Microsoft.EntityFrameworkCore;

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

    // GET /api/Analytics/priority-conflicts?userId=1
    [HttpGet("priority-conflicts")]
    public async Task<IActionResult> GetPriorityConflicts([FromQuery] int userId)
    {
        // Tamamlanmamış high priority görevler
        var tasks = await _db.Tasks
            .Where(t => t.UserId == userId
                        && t.Status != TaskManager.Api.Models.TaskStatus.Done
                        && t.Priority == PriorityLevel.High)
            .ToListAsync();

        // Deadline tarihine göre grupla (gün bazlı çakışma)
        var conflicts = tasks
            .GroupBy(t => t.Deadline.ToUniversalTime().Date)
            .Where(g => g.Count() >= 2)
            .Select(g => new
            {
                Date = g.Key,
                Count = g.Count(),
                Tasks = g.Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.EstimatedMinutes,
                    t.Deadline,
                    t.Status
                }).ToList()
            })
            .OrderBy(x => x.Date)
            .ToList();

        return Ok(new
        {
            HasConflict = conflicts.Any(),
            Conflicts = conflicts
        });
    }
    // GET /api/Analytics/recommendations?userId=1
    [HttpGet("recommendations")]
    public async Task<IActionResult> GetRecommendations([FromQuery] int userId)
    {
        var now = DateTime.UtcNow;

        var tasks = await _db.Tasks
            .Where(t => t.UserId == userId && t.Status != TaskManager.Api.Models.TaskStatus.Done)
            .ToListAsync();

        // Görev bağımlılıklarını çek
        var deps = await _db.TaskDependencies
            .Where(d => d.UserId == userId)
            .ToListAsync();

        // Basit öneri üretimi:
        // 1) Deadline yakın + High ise öne al
        // 2) Bir görev bağımlıysa önce bağımlı olduğu görevi bitir
        // 3) Çoklu high conflict varsa "en yakın deadline" önceliklendir
        var recommendations = tasks
            .Select(t =>
            {
                var hoursLeft = (t.Deadline.ToUniversalTime() - now).TotalHours;

                var dependsOn = deps.Where(d => d.TaskId == t.Id).Select(d => d.DependsOnTaskId).ToList();
                string suggestion;

                if (dependsOn.Any())
                {
                    suggestion = $"Önce bağımlı olduğun görev(ler)i tamamla: {string.Join(", ", dependsOn)}";
                }
                else if (t.Priority == PriorityLevel.High && hoursLeft <= 72)
                {
                    suggestion = "Yüksek öncelik ve yakın deadline: bunu sıradaki iş yap.";
                }
                else if (hoursLeft <= 24)
                {
                    suggestion = "Deadline çok yakın: süreyi bölerek hemen başla (pomodoro / küçük parçalara böl).";
                }
                else
                {
                    suggestion = "Plan normal: mevcut sıranı koruyabilirsin.";
                }

                return new
                {
                    t.Id,
                    t.Title,
                    t.Priority,
                    t.Deadline,
                    t.EstimatedMinutes,
                    Suggestion = suggestion
                };
            })
            .OrderBy(x => x.Deadline)
            .ToList();

        return Ok(recommendations);
    }
}
