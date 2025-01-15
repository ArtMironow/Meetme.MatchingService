using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Infrastructure.Common;
using Meetme.MatchingService.Infrastructure.Persistence.Repositories;
using Meetme.MatchingService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meetme.MatchingService.Infrastructure;


public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<MatchingServiceDbContext>(
			options => options
				.UseNpgsql(configuration.GetConnectionString(ConfigurationKeys.ConnectionString))
				.AddInterceptors(new TimestampInterceptor()));

		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

		return services;
	}
}
