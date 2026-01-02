using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;


var builder = WebApplication.CreateBuilder(args);

// Controller desteği (TasksController vs yazacağız)
builder.Services.AddControllers();

// Swagger UI için gerekli servisler
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS yönlendirme şimdilik kapalı
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// hızlı test endpointi
app.MapGet("/health", () => Results.Ok("OK"));

app.Run();
