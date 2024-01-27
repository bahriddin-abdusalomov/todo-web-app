using Microsoft.EntityFrameworkCore;

using Todo.API.Entities;

namespace Todo.API.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {}

    public DbSet<TaskModel> Tasks { get; set; }

    // Add DbSet properties for other entities if needed

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships or additional settings here

        // Example: modelBuilder.Entity<SomeEntity>().HasMany(e => e.RelatedEntities).WithOne(r => r.SomeEntity);
    }

}
