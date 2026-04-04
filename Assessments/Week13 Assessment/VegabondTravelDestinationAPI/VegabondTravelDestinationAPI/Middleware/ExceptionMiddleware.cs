namespace VegabondTravelDestinationAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            
            catch(Invalidrating ex)
            {
                context.Response.StatusCode = 400;

                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var result = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }

    [Serializable]
    internal class Invalidrating :Exception
    {
        public Invalidrating()
        {
        }

        public Invalidrating(string? message) : base(message)
        {
        }

        public Invalidrating(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}