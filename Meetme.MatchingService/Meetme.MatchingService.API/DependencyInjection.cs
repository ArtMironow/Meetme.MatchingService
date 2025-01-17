using Meetme.MatchingService.API.Common.Mappings;
using Meetme.MatchingService.API.Extensions;
using Meetme.MatchingService.API.Middleware;

namespace Meetme.MatchingService.API;

public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddMappings();

		services.ConfigureAuth();
		services.AddAuthorization();

		services.AddTransient<GlobalExceptionHandlingMiddleware>();

		return services;
	}
}
