using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Application.Services.Match;
using Meetme.MatchingService.Application.Services.Notification;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Meetme.MatchingService.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddScoped<IMatchService, MatchService>();
		services.AddScoped<INotificationService, NotificationService>();

		services.AddMediatR(cfg => 
			cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		return services;
	}
}
