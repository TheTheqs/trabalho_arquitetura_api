namespace WebApplication1.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using WebApplication1.DataBase;
	using WebApplication1.Models;

	//Declaração de controlador via Tags
	[ApiController]
	[Route("api/[controller]")]
	public class AlunoController : ControllerBase
	{
		private readonly AlunoData _data;
		public AlunoController(AlunoData data) => _data = data;

		// GET - sem argumentos de requisição, devolve lista dos alunos em banco.
		[HttpGet]
		public async Task<IActionResult> Get() =>
			Ok(await _data.ListarAsync());

		// GET - Recebe id, retorna o Aluno correspondente, se houver.
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var item = await _data.ObterAsync(id);
			return item is null ? NotFound() : Ok(item);
		}

		// POST - recebe formulário JSON em corpo que representa objeto entidade Aluno para ser salvo em banco.
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Aluno aluno)
		{
			var id = await _data.CriarAsync(aluno);
			return CreatedAtAction(nameof(GetById), new { id }, aluno);
		}

		// PUT - Recebe formulário JSON em corpo que representa ojeto entidade aluno, atualiza o mesmo já previmente salvo em banco
		// Método PUT, sobreescreve o objeto como um todo.
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Put(int id, [FromBody] Aluno aluno)
		{
			if (id != aluno.Id) return BadRequest("Id do corpo difere da rota.");
			var ok = await _data.AtualizarAsync(aluno);
			return ok ? NoContent() : NotFound();
		}

		//DELETE - Recebe id como requisição, remove o objeto do banco de dados.
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var ok = await _data.RemoverAsync(id);
			return ok ? NoContent() : NotFound();
		}
	}
}
