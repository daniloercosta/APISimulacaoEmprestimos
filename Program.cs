using APISimulacaoEmprestimos.Context;
using APISimulacaoEmprestimos.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<LoanSimulationService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Simulação de Empréstimos",
        Version = "v1",
        Description = "API para simulação de empréstimos, cálculo de parcelas e geração de propostas.",
        Contact = new OpenApiContact
        {
            Name = "Suporte",
            Email = "suporte@dominio.com",
            Url = new Uri("https://www.dominio.com/suporte")
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
