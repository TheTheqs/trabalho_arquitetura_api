using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

// Código por fazer o mapeamento de classe/entidade no EF
namespace WebApplication1.DataBase
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Aluno> Alunos { get; set; }
		public DbSet<Livro> Livros { get; set; }
	}
}
