using Microsoft.EntityFrameworkCore;
using EntityFrameworkExample.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EntityContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("EntityDb"))
);

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
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
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
