
using SimpleNameApi.Interfaces;
using SimpleNameApi.Repositories;
using SimpleNameApi.Services;
using SimpleNameApi.Middleware;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddSingleton<INameRepository, InMemoryNameRepository>();

builder.Services.AddScoped<INameService, NameService>();

builder.Services.AddOpenApi();

var app = builder.Build();


app.UseMiddleware<GlobalExceptionMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
