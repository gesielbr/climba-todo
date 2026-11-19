namespace ClimbaTodo.Application.DTOs;

public record UpdateTodoDto(
    string Title,
    string? Description,
    DateTime? DueDate,
    bool? IsCompleted
);