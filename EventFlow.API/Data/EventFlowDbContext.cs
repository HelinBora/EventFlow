
using EventFlow.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.API.Data;

public class EventFlowDbContext : DbContext
{
    public EventFlowDbContext(DbContextOptions<EventFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events => Set<Event>();
}
