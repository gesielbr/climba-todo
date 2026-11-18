
using ClimbaTodo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClimbaTodo.Infrastructure.Persistence;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TodoDbContext).Assembly
        );
    }
}
