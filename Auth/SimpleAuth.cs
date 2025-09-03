
// Classe responsável pena autenticação, feita de forma simplificada
namespace WebApplication1.Auth
{
	public class SimpleAuth
	{
		private readonly RequestDelegate _next;
		private readonly string _login;
		private readonly string _password;

		public SimpleAuth(RequestDelegate next, IConfiguration config)
		{
			_next = next;
			_login = config["Auth:Login"] ?? "";
			_password = config["Auth:Password"] ?? "";
		}

		public async Task InvokeAsync(HttpContext ctx)
		{
			// liberar Swagger e arquivos estáticos
			var path = ctx.Request.Path.Value?.ToLower() ?? "";
			if (path.StartsWith("/swagger") || path.StartsWith("/index.html"))
			{
				await _next(ctx);
				return;
			}

			// ler cabeçalhos
			var hasLogin = ctx.Request.Headers.TryGetValue("X-Login", out var login);
			var hasPass = ctx.Request.Headers.TryGetValue("X-Password", out var pass);

			if (!hasLogin || !hasPass || login != _login || pass != _password)
			{
				ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await ctx.Response.WriteAsync("Unauthorized: send X-Login and X-Password headers.");
				return;
			}

			await _next(ctx);
		}
	}
}
