﻿using MediatR;
using Meetme.MatchingService.API.Common.Mappings;
using Meetme.MatchingService.API.Extensions;
using Meetme.MatchingService.API.Middleware;
using Meetme.MatchingService.API.Notifications;
using Meetme.MatchingService.Domain.Events;
using System.Text.Json.Serialization;

namespace Meetme.MatchingService.API;

public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddSignalR().AddJsonProtocol(options =>
		{
			options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
		});

		services.AddScoped<INotificationHandler<NotificationEvent>, NotificationEventHandler>();

		services.AddMappings();

		services.ConfigureAuth();
		services.AddAuthorization();

		services.AddTransient<GlobalExceptionHandlingMiddleware>();

		return services;
	}
}
