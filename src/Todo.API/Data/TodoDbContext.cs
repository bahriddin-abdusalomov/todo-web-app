using Microsoft.EntityFrameworkCore;

namespace Todo.API.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {}

}
