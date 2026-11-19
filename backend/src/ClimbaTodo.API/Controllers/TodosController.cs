using ClimbaTodo.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using ClimbaTodo.Application.Services;

namespace ClimbaTodo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodosController(TodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todos = await _todoService.GetAllAsync();

        return Ok(todos);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoDto dto)
    {
        await _todoService.CreateAsync(dto);

        return Ok("Tarefa criada com sucesso.");
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var todo = await _todoService.GetByIdAsync(id);

        if (todo is null)
            return NotFound("Tarefa não encontrada.");

        return Ok(todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, UpdateTodoDto dto)
    {
        try
        {
            await _todoService.UpdateAsync(id, dto.Title, dto.Description, dto.DueDate, dto.IsCompleted);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Tarefa não encontrada." });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _todoService.DeleteAsync(id);

        return Ok("Tarefa excluída com sucesso.");
    }

    [HttpPatch("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        await _todoService.CompleteAsync(id);

        return Ok("Tarefa concluída com sucesso.");
    }

}