using api_filmes_senai.Context;
using api_filmes_senai.Interface;
using api_filmes_senai.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados (exemplo com SQL Server)
builder.Services.AddDbContext<Filme_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Adicionar o repositório e a interface ao container de injeção de dependência 
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();

//Adicionar o serviço de Controllers
builder.Services.AddControllers();

var app = builder.Build();

//Adicionar mapeamento dos controllers
app.MapControllers();

app.Run();
