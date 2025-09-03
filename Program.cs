// Os comentários em inglês são padrão de projeto API Web .NET core do Visual Studio Community
// "usings" aplicados como imports para o uso do DbContext, que será nosso orquestrador do banco de dados.
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataBase;
//Abaixo o usings do sistema de autenticação
using WebApplication1.Auth;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Alteração no SwaggerGen para integrar a autenticação de forma simplificada.
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

	// Definições para dois headers
	c.AddSecurityDefinition("X-Login", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "X-Login",
		Type = SecuritySchemeType.ApiKey,
		Description = "Seu último sobrenome"
	});
	c.AddSecurityDefinition("X-Password", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "X-Password",
		Type = SecuritySchemeType.ApiKey,
		Description = "Seu RU"
	});

	// Requisito: ambos os headers em todas as operações
	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
		{ new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "X-Login" } }, new string[]{} },
		{ new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "X-Password" } }, new string[]{} }
	});
});

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

app.UseMiddleware<SimpleAuth>();

app.MapControllers();

app.Run();
