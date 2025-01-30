using Meetme.MatchingService.API;
using Meetme.MatchingService.API.Common.Notifications;
using Meetme.MatchingService.API.Middleware;
using Meetme.MatchingService.API.Notifications;
using Meetme.MatchingService.Application;
using Meetme.MatchingService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddApplication()
	.AddInfrastructure(builder.Configuration)
	.AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapHub<NotificationsHub>(NotificationsKeys.NotificationsEndpoint);

app.MapControllers();

app.Run();
