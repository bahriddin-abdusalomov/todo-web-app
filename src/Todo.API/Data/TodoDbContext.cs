using Microsoft.EntityFrameworkCore;

using Todo.API.Entities;

namespace Todo.API.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {}

    public DbSet<TaskModel> Tasks { get; set; }
}
