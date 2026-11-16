
using SimpleNameApi.Interfaces;
using SimpleNameApi.Repositories;
using SimpleNameApi.Services;
using SimpleNameApi.Middleware;
using SimpleNameApi.Configurations;
using SimpleNameApi.Data;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection(MongoDBSettings.SectionName)
);

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddSingleton<INameRepository, MongoNameRepository>();

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
