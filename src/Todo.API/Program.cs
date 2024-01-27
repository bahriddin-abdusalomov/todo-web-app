using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

using Todo.API.Data;
using Todo.API.Interfaces;
using Todo.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Add services to the container.

// Web api project connection with Blazor project
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowBlazorClient", builder =>
//    {
//        builder.AllowAnyOrigin() // Blazor proyektningizning URL manzili
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//    });
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7039", "https://localhost:7039")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);


app.MapControllers();

app.Run();
