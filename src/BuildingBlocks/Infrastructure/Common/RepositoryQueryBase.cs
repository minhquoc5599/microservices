using Contract.Common.Interfaces;
using Contract.Domains.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Common
{
	public class RepositoryQueryBase<T, K, TContext> : IRepositoryQueryBase<T, K, TContext> where T : EntityBase<K>
		where TContext : DbContext
	{
		private readonly TContext _context;

		public RepositoryQueryBase(TContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(_context));
		}

		public IQueryable<T> FindAll(bool trackChanges = false)
		{
			if (trackChanges)
			{
				return _context.Set<T>();
			}
			return _context.Set<T>().AsNoTracking();
		}

		public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
		{
			var items = FindAll(trackChanges);
			items = includeProperties.Aggregate(items, (current, includeProperty) =>
			{
				return current.Include(includeProperty);
			});
			return items;
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
		{
			if (trackChanges)
			{
				return _context.Set<T>().Where(expression);
			}
			return _context.Set<T>().Where(expression).AsNoTracking();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
			params Expression<Func<T, object>>[] includeProperties)
		{
			var items = FindByCondition(expression, trackChanges);
			items = includeProperties.Aggregate(items, (current, includeProperty) =>
			{
				return current.Include(includeProperty);
			});
			return items;
		}

		public async Task<T?> GetByIdAsync(K id)
		{
			return await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();
		}

		public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
		{
			return await FindByCondition(
				x => x.Id.Equals(id), trackChanges: false, includeProperties).FirstOrDefaultAsync();
		}
	}
}
