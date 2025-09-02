namespace WebApplication1.Models
{
	// Entidade principal do trabalho, possui atributos mínimos relacionados aos atributos fornecidos pelo roteiro da atividade prática.
	public class Livro
	{
		public int Id { get; set; }
		public string Autor { get; set; }
		public string Titulo { get; set; }
		public string Editora { get; set; }
		public int Ano { get; set; }

	}
}
