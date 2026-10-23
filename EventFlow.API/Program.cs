
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var events = new[]
{
    new
    {
        Id = 1,
        Name = "Yazılım Geliştirici Buluşması",
        Venue = "İstanbul",
        StartsAt = new DateTimeOffset(2026, 11, 15, 19, 0, 0, TimeSpan.FromHours(3)),
        AvailableTickets = 120
    },
    new
    {
        Id = 2,
        Name = "Backend ve Veritabanı Zirvesi",
        Venue = "Ankara",
        StartsAt = new DateTimeOffset(2026, 12, 5, 14, 0, 0, TimeSpan.FromHours(3)),
        AvailableTickets = 80
    }
};

app.MapGet("/api/events", () => events)
    .WithName("GetEvents");

app.Run();
