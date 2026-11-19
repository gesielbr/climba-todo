namespace ClimbaTodo.Application.DTOs;

public record CreateTodoDto(
    string Title,
    string? Description,
    DateTime? DueDate
);