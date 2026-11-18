
using ClimbaTodo.Application.Interfaces;
using ClimbaTodo.Domain.Entities;
using ClimbaTodo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClimbaTodo.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<Todo?> GetByIdAsync(Guid id)
    {
        return await _context.Todos
            .FirstOrDefaultAsync(todo => todo.Id == id);
    }

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
        return await _context.Todos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Todo todo)
    {
        await _context.Todos.AddAsync(todo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Todo todo)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Todo todo)
    {
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
    }
}
