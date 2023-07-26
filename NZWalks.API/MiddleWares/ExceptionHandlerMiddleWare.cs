using System.Net;

namespace NZWalks.API.MiddleWares;

public class ExceptionHandlerMiddleWare
{
    private readonly ILogger<ExceptionHandlerMiddleWare> logger;
    private readonly RequestDelegate next;

    public ExceptionHandlerMiddleWare(ILogger<ExceptionHandlerMiddleWare> logger, RequestDelegate next)
    {
        this.logger = logger;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }catch (Exception ex)
        {
            var errorId = Guid.NewGuid();

            logger.LogError(ex, "{errorId} : {message}", errorId, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var error = new
            {
                Id = errorId,
                ErrorMessage = "Something went wrong !"
            };

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
