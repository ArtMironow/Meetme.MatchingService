using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Infrastructure.Common;
using Meetme.MatchingService.Infrastructure.Persistence.Repositories;
using Meetme.MatchingService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Meetme.MatchingService.Infrastructure.ExternalServices;
using Meetme.MatchingService.Infrastructure.Persistence.Configurations;

namespace Meetme.MatchingService.Infrastructure;


public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<ProfileServiceRoutes>(configuration.GetSection(ConfigurationKeys.ProfileServiceRoutesSection));

		services.Configure<ConnectionStrings>(configuration.GetSection(ConfigurationKeys.ConnectionStringsSection));

		services.AddHttpContextAccessor();

		services.AddHttpClient<IProfileServiceClient, ProfileServiceClient>().AddStandardResilienceHandler();

		services.AddScoped<IProfileServiceClient, ProfileServiceClient>();

		services.AddDbContext<MatchingServiceDbContext>(
			options => options
				.UseNpgsql(configuration.GetConnectionString(ConfigurationKeys.ConnectionString))
				.AddInterceptors(new TimestampInterceptor()));

		MongoDbConfiguration.Configure();

		services.AddScoped<IMongoRepository, MongoRepository>();

		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

		return services;
	}
}
