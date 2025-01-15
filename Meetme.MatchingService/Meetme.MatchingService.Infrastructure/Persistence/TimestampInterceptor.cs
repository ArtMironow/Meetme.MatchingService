using Meetme.MatchingService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Meetme.MatchingService.Infrastructure.Persistence;

public sealed class TimestampInterceptor : SaveChangesInterceptor
{
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
		DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken = default)
	{
		UpdateTimestamps(eventData.Context);

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private static void UpdateTimestamps(DbContext? context)
	{
		if (context == null) return;

		foreach (var entry in context.ChangeTracker.Entries())
		{
			if (entry.Entity is MatchEntity entity)
			{
				var utcNow = DateTime.UtcNow;

				if (entry.State == EntityState.Added)
				{
					entity.MatchDate = utcNow;
				}
			}
		}
	}
}
