using DirectoryService.Application.Locations;
using DirectoryService.Infrastructure;
using DirectoryService.Infrastructure.Repositories;
using DirectoryService.Presentation.Exeptions;
using Microsoft.OpenApi.Models;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformer((schema, context, _) =>
    {
        if (context.JsonTypeInfo.Type == typeof(Envelope<Errors>))
        {
            if (schema.Properties.TryGetValue("errors", out var errorsProp))
            {
                errorsProp.Items.Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "Errors", };
            }
        }

        return Task.CompletedTask;
    });
});
builder.Services.AddScoped<DirectoryServiceDbContext>(
    _ => new DirectoryServiceDbContext(builder.Configuration.GetConnectionString("DirectoryServiceDb")!));

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