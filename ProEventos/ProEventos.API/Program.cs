using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Interfaces;
using ProEventos.Application.Interfaces.Implementations;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Interfaces.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ProEventosContext>(
    context => context.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



//Injeção de Dependencia
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IGeralRepository, GeralRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
