namespace WebApplication1.DataBase
{
	using Microsoft.EntityFrameworkCore;
	using WebApplication1.Models;
	public class LivroData
	{
		private readonly AppDbContext _db;
		public LivroData(AppDbContext db) => _db = db; //construtor
		
		//C - Create
		public async Task<int> CriarAsync(Livro livro)
		{
			_db.Livros.Add(livro);
			await _db.SaveChangesAsync();
			return livro.Id;
		}

		//R - Read
		public async Task<List<Livro>> ListarAsync() =>
			await _db.Livros.AsNoTracking().ToListAsync();

		public async Task<Livro?> ObterAsync(int id) =>
			await _db.Livros.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

		//U - Update
		public async Task<bool> AtualizarAsync(Livro livro)
		{
			_db.Livros.Update(livro);
			return await _db.SaveChangesAsync() > 0;
		}

		//D - Delete
		public async Task<bool> RemoverAsync(int id)
		{
			var entity = await _db.Livros.FindAsync(id);
			if (entity is null) return false;
			_db.Livros.Remove(entity);
			return await _db.SaveChangesAsync() > 0;
		}
	}
}
