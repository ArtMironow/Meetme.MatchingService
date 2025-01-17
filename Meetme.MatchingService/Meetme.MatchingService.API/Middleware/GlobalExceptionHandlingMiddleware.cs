using Meetme.MatchingService.Domain.Common.Errors;
using System.Net;
using Meetme.MatchingService.Domain.Common.Exceptions;

namespace Meetme.MatchingService.API.Middleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			var statusCode = ex switch
			{
				EntityNotFoundException => HttpStatusCode.NotFound,
				_ => HttpStatusCode.InternalServerError
			};

			context.Response.StatusCode = (int)statusCode;

			var errorDetails = new ErrorDetails
			{
				ErrorTitle = "Server error",
				ErrorMessage = ex.Message
			};

			await context.Response.WriteAsJsonAsync(errorDetails);
		}
	}
}
