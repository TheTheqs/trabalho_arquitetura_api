namespace WebApplication1.DataBase
	//Classe repositório que vai comunicar o banco para execução de todo CRUD, ela é a ponte entre o controller e o banco em si.
{
	using Microsoft.EntityFrameworkCore;
	using WebApplication1.Models;

	public class AlunoData
	{
		private readonly AppDbContext _db; //contexto de acesso ao banco
		public AlunoData(AppDbContext db) => _db = db; //construtor

		// C - Create
		public async Task<int> CriarAsync(Aluno aluno)
		{
			_db.Alunos.Add(aluno);
			await _db.SaveChangesAsync();
			return aluno.Id;
		}

		// R - Read
		public async Task<List<Aluno>> ListarAsync() => //Lista todos os Alunos
			await _db.Alunos.AsNoTracking().ToListAsync();

		public async Task<Aluno?> ObterAsync(int id) =>
			await _db.Alunos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id); //Retorna o aluno com o id fornecido, caso exista.

		// U - Update
		public async Task<bool> AtualizarAsync(Aluno aluno) //No estilo PUT, recebe o objeto todo.
		{
			_db.Alunos.Update(aluno);
			return await _db.SaveChangesAsync() > 0;
		}

		//D - Destroy ou Delete :D
		public async Task<bool> RemoverAsync(int id)
		{
			var entity = await _db.Alunos.FindAsync(id);
			if (entity is null) return false;
			_db.Alunos.Remove(entity);
			return await _db.SaveChangesAsync() > 0;
		}
	}
}
