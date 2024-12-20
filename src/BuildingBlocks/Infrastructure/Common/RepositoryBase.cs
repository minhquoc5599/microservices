﻿using Contract.Common.Interfaces;
using Contract.Domains.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common
{
	public class RepositoryBase<T, K, TContext> : RepositoryQueryBase<T, K, TContext>,
		IRepositoryBase<T, K, TContext>
		where T : EntityBase<K> where TContext : DbContext
	{
		private readonly TContext _context;
		private readonly IUnitOfWork<TContext> _unitOfWork;

		public RepositoryBase(TContext context, IUnitOfWork<TContext> unitOfWork) : base(context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(_context));
			_unitOfWork = unitOfWork;
		}

		public async Task<K> CreateAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return entity.Id;
		}

		public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
		{
			await _context.Set<T>().AddRangeAsync(entities);
			return entities.Select(x => x.Id).ToList();
		}

		public Task UpdateAsync(T entity)
		{
			if (_context.Entry(entity).State == EntityState.Unchanged) return Task.CompletedTask;

			T checkExist = _context.Set<T>().Find(entity.Id);
			_context.Entry(checkExist).CurrentValues.SetValues(entity);

			return Task.CompletedTask;
		}

		public Task UpdateListAsync(IEnumerable<T> entities)
		{
			return _context.Set<T>().AddRangeAsync(entities);
		}

		public Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
			return Task.CompletedTask;
		}

		public Task DeleteListAsync(IEnumerable<T> entities)
		{
			_context.Set<T>().RemoveRange(entities);
			return Task.CompletedTask;
		}

		public Task<int> SaveChangesAsync()
		{
			return _unitOfWork.CommitAsync();
		}

		public Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return _context.Database.BeginTransactionAsync();
		}

		public async Task EndTransactionAsync()
		{
			await SaveChangesAsync();
			await _context.Database.CommitTransactionAsync();
		}

		public Task RollbackTransactionAsync()
		{
			return _context.Database.RollbackTransactionAsync();
		}
	}
}
