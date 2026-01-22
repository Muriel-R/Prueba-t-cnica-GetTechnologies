using GetechnologiesTest.Application;
using GetechnologiesTest.Application.Abstractions;
using GetechnologiesTest.Infrastructure;
using GetechnologiesTest.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using GetechnologiesTest.Application.Services;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repos
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<DirectorioService>();
builder.Services.AddScoped<VentasService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
