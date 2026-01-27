using DirectoryService.Application.Locations;
using DirectoryService.Infrastructure;
using DirectoryService.Infrastructure.Database;
using DirectoryService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<DirectoryServiceDbContext>(
    _ => new DirectoryServiceDbContext(builder.Configuration.GetConnectionString("DirectoryServiceDb")!));

// builder.Services.AddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>();
// builder.Services.AddScoped<ILocationsRepository, NpgSqlLocationsRepository>(); 
builder.Services.AddScoped<ILocationsRepository, EfCoreLocationsRepository>();

builder.Services.AddScoped<CreateLocationHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Directory Service "));
}

app.MapControllers();

app.Run();