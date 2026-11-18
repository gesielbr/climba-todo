using ClimbaTodo.Application.Interfaces;
using ClimbaTodo.Infrastructure.Repositories;
using ClimbaTodo.Application.Services;
using Scalar.AspNetCore;
using ClimbaTodo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<TodoService>();
var envPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "..", ".env")
);

if (File.Exists(envPath))
{
    DotNetEnv.Env.Load(envPath);
}

DotNetEnv.Env.Load(envPath);
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

// Add services to the container.

builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

app.Run($"http://0.0.0.0:{port}");
