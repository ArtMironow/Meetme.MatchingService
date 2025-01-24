using System.Linq.Expressions;

namespace Meetme.MatchingService.Application.Common.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
	Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
	Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
	Task AddAsync(TEntity entity, CancellationToken cancellationToken);
	Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
}
