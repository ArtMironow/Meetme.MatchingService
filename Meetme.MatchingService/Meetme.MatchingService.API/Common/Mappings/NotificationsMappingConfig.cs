using Mapster;
using Meetme.MatchingService.Application.Notifications.Queries.GetByUserId;
using Meetme.MatchingService.Contracts.Notifications.GetByUserId;
using Meetme.MatchingService.Domain.Events;

namespace Meetme.MatchingService.API.Common.Mappings;

public class NotificationsMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<NotificationEvent, GetNotificationsByUserIdResponse>()
			.Map(dest => dest.MatchedProfileId, src => src.EventDetails != null ? src.EventDetails.MatchedProfileId : null)
			.Map(dest => dest.Type, src => src.Type.ToString());

		config.NewConfig<GetNotificationsByUserIdResult, IEnumerable<GetNotificationsByUserIdResponse>>()
			.MapWith(src => src.Notifications.Select(notification => notification.Adapt<GetNotificationsByUserIdResponse>()));
			
	}
}
