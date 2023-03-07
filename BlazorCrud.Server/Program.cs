using BlazorCrud.Server.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obtenemos la cadena de conexión desde el AppSettings.json
builder.Services.AddDbContext<DbcrudBlazorContext>( opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("nuevaPolitica", app =>
    {
        // Habilitamos los cors para que permita cualesquier petición
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuramos la nueva politica
app.UseCors("nuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
