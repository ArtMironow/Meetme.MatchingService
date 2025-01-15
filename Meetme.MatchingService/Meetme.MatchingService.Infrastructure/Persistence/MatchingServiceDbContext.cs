using Meetme.MatchingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetme.MatchingService.Infrastructure.Persistence;


public class MatchingServiceDbContext : DbContext
{
	public MatchingServiceDbContext(DbContextOptions<MatchingServiceDbContext> options)
		: base(options)
	{
		Database.Migrate();
	}

	public DbSet<LikeEntity>? Likes { get; set; }
	public DbSet<MatchEntity>? Matches { get; set; }
}
