using Microsoft.AspNetCore.Http.HttpResults;
using ucondo_challenge.business.Exceptions;

namespace ucondo_challenge.api.Middleware
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
			catch (NotFoundException notFound) {
                logger.LogError(notFound, notFound.Message);
				context.Response.StatusCode = 404;
				await context.Response.WriteAsync(notFound.Message);

				logger.LogWarning(notFound.Message);
            }
            catch (BadRequestException badRequest)
            {
                logger.LogError(badRequest, badRequest.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequest.Message);

                logger.LogWarning(badRequest.Message);
            }            
            catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Something went wrong");				
			}
        }
    }
}
