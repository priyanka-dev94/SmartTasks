using SmartTasks.API.Middlewares;
using SmartTasks.Application.Services;
using SmartTasks.Infrastructure.Extensions;
using TaskManagementSystem.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Layered DI
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

//AutoMapper
builder.Services.AddAutoMapper(typeof(TaskService).Assembly, typeof(Program).Assembly);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
