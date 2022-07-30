using Message.Consumer.DAL.Models;
using Message.Shared.Deduplication;
using Microsoft.EntityFrameworkCore;

namespace Message.Consumer.DAL;

public class NotifyDbContext : DbContext
{
    public DbSet<UserCreatedModel> UserCreated { get; set; }
    public DbSet<DeduplicationModel> Deduplications { get; set; }
    
    public NotifyDbContext(DbContextOptions<NotifyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}