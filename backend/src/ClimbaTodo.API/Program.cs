using ClimbaTodo.Application.Interfaces;
using ClimbaTodo.Infrastructure.Repositories;
using ClimbaTodo.Application.Services;
using Scalar.AspNetCore;
using ClimbaTodo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS — permitir o frontend Angular local
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularLocal",
        policy =>
        {
            policy.WithOrigins(
    "http://localhost:4200",
    "http://localhost:58365",
    "https://limba-todo-frontend.vercel.app",
    "https://climba-todo-frontend.vercel.app"
)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddScoped<TodoService>();

var envPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "..", ".env")
);

if (File.Exists(envPath))
{
    DotNetEnv.Env.Load(envPath); // apenas uma vez
}

var connectionString = new Npgsql.NpgsqlConnectionStringBuilder
{
    Host = Environment.GetEnvironmentVariable("PGHOST")
        ?? throw new InvalidOperationException("PGHOST não configurado."),

    Database = Environment.GetEnvironmentVariable("PGDATABASE")
        ?? throw new InvalidOperationException("PGDATABASE não configurado."),

    Username = Environment.GetEnvironmentVariable("PGUSER")
        ?? throw new InvalidOperationException("PGUSER não configurado."),

    Password = Environment.GetEnvironmentVariable("PGPASSWORD")
        ?? throw new InvalidOperationException("PGPASSWORD não configurado."),

    SslMode = Npgsql.SslMode.VerifyFull,
    ChannelBinding = Npgsql.ChannelBinding.Require
}.ConnectionString;

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseRouting();                         // garante o roteamento antes do CORS
app.UseCors("AllowAngularLocal");         // ⬅️ ESSENCIAL: ativa o CORS

app.UseAuthorization();
app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");