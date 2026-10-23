using EventFlow.API.Data;
using Microsoft.EntityFrameworkCore;
using EventFlow.API.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EventFlowDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EventFlowDbContext>();

    if (!await db.Events.AnyAsync())
    {
        db.Events.AddRange(
            new Event
            {
                Name = "Yazılım Geliştirici Buluşması",
                Venue = "İstanbul",
                StartsAt = new DateTimeOffset(2026, 11, 15, 19, 0, 0, TimeSpan.FromHours(3)),
                AvailableTickets = 120
            },
            new Event
            {
                Name = "Backend ve Veritabanı Zirvesi",
                Venue = "Ankara",
                StartsAt = new DateTimeOffset(2026, 12, 5, 14, 0, 0, TimeSpan.FromHours(3)),
                AvailableTickets = 80
            }
        );

        await db.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/api/events", async (EventFlowDbContext db) =>
    await db.Events.ToListAsync())
    .WithName("GetEvents");

app.Run();
