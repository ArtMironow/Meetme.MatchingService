using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Meetme.MatchingService.API.Notifications;

[Authorize]
public class NotificationsHub : Hub
{
}
