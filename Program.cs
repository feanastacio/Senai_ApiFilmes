using System;
using System.Reflection;
using api_filmes_senai.Context;
using api_filmes_senai.Interface;
using api_filmes_senai.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados (exemplo com SQL Server)
builder.Services.AddDbContext<Filme_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Adicionar o repositório e a interface ao container de injeção de dependência 
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Adicionar o serviço de Controllers
builder.Services.AddControllers();

//Adicionar serviço de JWT Bearer
builder.Services.AddAuthentication(options =>

{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida quem ta solicitando
        ValidateIssuer = true,

        //valida quem ta recebendo
        ValidateAudience = true,

        //define-se o tempo de expiração será validado
        ValidateLifetime = true,

        //fotrma de criptografia e valida a chave de autentificação 
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autentificacao-webapi-dev")),

        //valida o tempo de expiração 
        ClockSkew = TimeSpan.FromMinutes(5),

        //valida de onde está vindo
        ValidIssuer = "api_filmes_senai",

        ValidAudience = "api_filmes_senai"

        //
    };
});

//Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "Aplicação para gerenciamento de filmes e gêneros",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Fernanda Marques",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });

    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
//Adicionar mapeamento dos controllers
app.MapControllers();

//Adicionar Autenticação
app.UseAuthentication();

//Adicionar Autorização
app.UseAuthorization();


app.Run();
