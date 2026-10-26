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
{
    var now = DateTimeOffset.UtcNow;

    return await db.Events
        .Where(e => e.StartsAt > now)
        .OrderBy(e => e.StartsAt)
        .ToListAsync();
})
.WithName("GetEvents");

app.MapGet("/api/events/archive", async (EventFlowDbContext db) =>
{
    var now = DateTimeOffset.UtcNow;

    return await db.Events
        .Where(e => e.StartsAt <= now)
        .OrderByDescending(e => e.StartsAt)
        .ToListAsync();
})
.WithName("GetArchivedEvents");

app.MapGet("/api/events/{id:int}", async Task<IResult> (
    int id,
    EventFlowDbContext db) =>
{
    var eventItem = await db.Events.FindAsync(id);

    if (eventItem is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(eventItem);
})
.WithName("GetEventById");


app.MapPut("/api/events/{id:int}", async (
    int id,
    CreateEventRequest request,
    EventFlowDbContext db) =>
{
    var eventItem = await db.Events.FindAsync(id);

    if (string.IsNullOrWhiteSpace(request.Name) ||
    string.IsNullOrWhiteSpace(request.Venue))
   {
        return Results.BadRequest(
            "Etkinlik adı ve mekânı boş olamaz.");
    }

    if (eventItem is null)
    {
        return Results.NotFound();
    }

    if (string.IsNullOrWhiteSpace(request.Name) ||
        string.IsNullOrWhiteSpace(request.Venue))
    {
        return Results.BadRequest(
            "Etkinlik adı ve mekânı boş olamaz.");
    }

    if (request.StartsAt <= DateTimeOffset.UtcNow)
    {
        return Results.BadRequest(
            "Etkinlik başlangıç tarihi gelecekte olmalıdır.");
    }

    if (request.AvailableTickets < 0)
    {
        return Results.BadRequest(
            "Bilet sayısı negatif olamaz.");
    }

    eventItem.Name = request.Name;
    eventItem.Venue = request.Venue;
    eventItem.StartsAt = request.StartsAt;
    eventItem.AvailableTickets = request.AvailableTickets;

    await db.SaveChangesAsync();

    return Results.Ok(eventItem);
})
.WithName("UpdateEvent");


app.MapPost("/api/events", async (
    CreateEventRequest request,
    EventFlowDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(request.Name) ||
        string.IsNullOrWhiteSpace(request.Venue))
    {
        return Results.BadRequest(
            "Etkinlik adı ve mekânı boş olamaz.");
    }

    if (request.StartsAt <= DateTimeOffset.UtcNow)
{
    return Results.BadRequest(
        "Etkinlik başlangıç tarihi gelecekte olmalıdır.");
}

    if (request.AvailableTickets < 0)
    {
        return Results.BadRequest(
            "Bilet sayısı negatif olamaz.");
    }

    var newEvent = new Event
    {
        Name = request.Name,
        Venue = request.Venue,
        StartsAt = request.StartsAt,
        AvailableTickets = request.AvailableTickets
    };

    db.Events.Add(newEvent);
    await db.SaveChangesAsync();

    return Results.Created(
        $"/api/events/{newEvent.Id}",
        newEvent);
})
.WithName("CreateEvent");

app.MapDelete("/api/events/{id:int}", async (
    int id,
    EventFlowDbContext db) =>
{
    var eventItem = await db.Events.FindAsync(id);

    if (eventItem is null)
    {
        return Results.NotFound();
    }

    db.Events.Remove(eventItem);
    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithName("DeleteEvent");

app.Run();
