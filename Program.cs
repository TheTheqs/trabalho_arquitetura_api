// Os comentários em inglês são padrão de projeto API Web .NET core do Visual Studio Community
// "usings" aplicados como imports para o uso do DbContext, que será nosso orquestrador do banco de dados.
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuração de integração do entity framework no builder usando os usings importados.
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Inlcusão das classe Data, nossos repositórios
builder.Services.AddScoped<LivroData>();
builder.Services.AddScoped<AlunoData>();

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
