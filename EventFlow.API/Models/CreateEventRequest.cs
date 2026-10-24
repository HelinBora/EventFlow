namespace EventFlow.API.Models;

public class CreateEventRequest
{
    public string Name { get; set; } = string.Empty;

    public string Venue { get; set; } = string.Empty;

    public DateTimeOffset StartsAt { get; set; }

    public int AvailableTickets { get; set; }
}