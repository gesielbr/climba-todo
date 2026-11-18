using ClimbaTodo.Domain.Entities;
using ClimbaTodo.Application.Interfaces;
using ClimbaTodo.Application.DTOs;

namespace ClimbaTodo.Application.Services;

public class TodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
        return await _todoRepository.GetAllAsync();
    }

    public async Task<Todo?> GetByIdAsync(Guid id)
    {
        return await _todoRepository.GetByIdAsync(id);
    }

    public async Task CreateAsync(CreateTodoDto dto)
    {
        var todo = new Todo(dto.Title, dto.Description);

        await _todoRepository.AddAsync(todo);
    }

    public async Task UpdateAsync(Guid id, string title, string? description)
    {
        var todo = await _todoRepository.GetByIdAsync(id);

        if (todo is null)
            throw new KeyNotFoundException("Tarefa não encontrada.");

        todo.Update(title, description);

        await _todoRepository.UpdateAsync(todo);
    }

    public async Task DeleteAsync(Guid id)
    {
        var todo = await _todoRepository.GetByIdAsync(id);

        if (todo is null)
            throw new KeyNotFoundException("Tarefa não encontrada.");

        await _todoRepository.DeleteAsync(todo);
    }

    public async Task CompleteAsync(Guid id)
    {
        var todo = await _todoRepository.GetByIdAsync(id);

        if (todo is null)
            throw new KeyNotFoundException("Tarefa não encontrada.");

        todo.Complete();

        await _todoRepository.UpdateAsync(todo);
    }
}