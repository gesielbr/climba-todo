namespace ClimbaTodo.Domain.Entities;

public class Todo
{
    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public DateTime? DueDate { get; private set; }  // 👈 NOVO

    public bool IsCompleted { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("O título é obrigatório.");

        if (title.Length > 150)
            throw new ArgumentException("O título não pode ultrapassar 150 caracteres.");
    }

    private static void ValidateDescription(string? description)
    {
        if (description?.Length > 1000)
            throw new ArgumentException("A descrição não pode ultrapassar 1000 caracteres.");
    }

    public Todo(string title, string? description = null, DateTime? dueDate = null)
    {
        ValidateTitle(title);
        ValidateDescription(description);
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (IsCompleted)
            return;

        IsCompleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string title, string? description, DateTime? dueDate = null, bool? isCompleted = null)
    {
        ValidateTitle(title);
        ValidateDescription(description);

        Title = title;
        Description = description;
        DueDate = dueDate;

        if (isCompleted.HasValue)
            IsCompleted = isCompleted.Value;

        UpdatedAt = DateTime.UtcNow;
    }
}