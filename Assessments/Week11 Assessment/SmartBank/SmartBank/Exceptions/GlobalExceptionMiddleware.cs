using System.Net;
using System.Text.Json;

namespace SmartBank.Exceptions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HttpStatusCode status;

                if (ex is NotFoundException)
                    status = HttpStatusCode.NotFound;
                else if (ex is BadRequestException)
                    status = HttpStatusCode.BadRequest;
                else
                    status = HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(new
                {
                    error = ex.Message
                });

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)status;

                await context.Response.WriteAsync(result);
            }
        }
    }
}
