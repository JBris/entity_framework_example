using Microsoft.EntityFrameworkCore;
using EntityFrameworkExample.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

String connectionString = Environment.GetEnvironmentVariable("DB");

if(connectionString == "sqlite") { 
    Console.WriteLine($"Using database connection {connectionString}");
    builder.Services.AddDbContext<EntityContext>(
        options => options.UseSqlite(builder.Configuration.GetConnectionString(connectionString))
    );
} else {
    connectionString = "postgres";
    Console.WriteLine($"Using database connection {connectionString}");

    builder.Services.AddDbContext<EntityContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString(connectionString))
    );
}

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

using(var scope = app.Services.CreateScope())
{
    var entityContext = scope.ServiceProvider.GetRequiredService<EntityContext>();
    entityContext.Database.EnsureDeleted();
    entityContext.Database.EnsureCreated();
    entityContext.Seed();
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
