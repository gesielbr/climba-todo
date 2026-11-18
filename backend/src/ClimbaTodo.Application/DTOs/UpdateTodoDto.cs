namespace ClimbaTodo.Application.DTOs;

public record UpdateTodoDto(
    string Title,
    string? Description
);