namespace Api01.Middleware
{
    public class LogRequisicaoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogRequisicaoMiddleware> _log;
        public LogRequisicaoMiddleware(RequestDelegate next, ILogger<LogRequisicaoMiddleware> log)
        {
            _next = next;
            _log = log;
        }
        public async Task InvokeAsync(HttpContext context )
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Ocorreu um erro ao processar a requisição.");

                if (context.Response.HasStarted)
                {
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var erro = new
                {
                    Mensagem = "Ocorreu um erro interno no servidor.",
                    Status = context.Response.StatusCode,
                    context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(erro);
            }
        }
    }
}
