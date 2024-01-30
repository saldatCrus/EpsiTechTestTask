using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace EpsiTechTestTask.Servises.Exeption
{
    public class ExceptionHandler : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Exception? exception = null;

            try
            {
                await next(context);
            }
            catch (Exception e)
            {

                exception = e;
            }
            finally
            {
                if (exception is not null)
                {
                    var (code, message, statusCode) = exception switch
                    {
                        ValidationException ex => (102, ex.Message, 400),
                        ApplicationException ex => (101, ex.Message, 400),
                        JsonException ex => (101, ex.Message, 400),
                        _ => (0, "unknown error", 500)

                    };

                    _logger.LogError(code, "Exception while handle request");
                    context.Response.Clear();
                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(new { message = message, code = code });
                }
            }
        }
    }
}
