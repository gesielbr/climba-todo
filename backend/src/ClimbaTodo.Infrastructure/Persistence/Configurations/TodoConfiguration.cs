
using ClimbaTodo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClimbaTodo.Infrastructure.Persistence.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("todos");

        builder.HasKey(todo => todo.Id);

        builder.Property(todo => todo.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(todo => todo.Description)
            .HasMaxLength(1000);

        builder.Property(todo => todo.IsCompleted)
            .IsRequired();

        builder.Property(todo => todo.CreatedAt)
            .IsRequired();

        builder.Property(todo => todo.UpdatedAt);
    }
}
