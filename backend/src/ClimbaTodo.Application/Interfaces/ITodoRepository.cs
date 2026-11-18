
using ClimbaTodo.Domain.Entities;

namespace ClimbaTodo.Application.Interfaces;

public interface ITodoRepository
{
    Task<Todo?> GetByIdAsync(Guid id);

    Task<IEnumerable<Todo>> GetAllAsync();

    Task AddAsync(Todo todo);

    Task UpdateAsync(Todo todo);

    Task DeleteAsync(Todo todo);
}
