using Microsoft.EntityFrameworkCore;
using Izumu.Api;
using Izumu.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Registrar Inyección de Dependencias
builder.Services.AddScoped<IClienteService, ClienteService>();

// 3. Habilitar CORS para que el Frontend pueda conectarse
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();