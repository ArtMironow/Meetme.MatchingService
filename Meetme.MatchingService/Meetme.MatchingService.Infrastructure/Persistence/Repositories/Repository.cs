using Meetme.MatchingService.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetme.MatchingService.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
	private readonly MatchingServiceDbContext _context;

	public Repository(MatchingServiceDbContext context)
	{
		_context = context;
	}

	public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await _context.Set<TEntity>().FindAsync(id, cancellationToken);
	}

	public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _context.Set<TEntity>().ToListAsync(cancellationToken);
	}

	public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
	{
		await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
	{
		_context.Set<TEntity>().Remove(entity);
		return _context.SaveChangesAsync(cancellationToken);
	}
}
