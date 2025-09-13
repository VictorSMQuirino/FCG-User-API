using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Interfaces.Repositories;
using FCG_Users.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FCG_Users.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
	private readonly FCGUsersDbContext _context;
	private readonly DbSet<T> _dbSet;

	protected BaseRepository(FCGUsersDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<T>();
	}

	public async Task CreateAsync(T entity)
	{
		await _dbSet.AddAsync(entity);
		await _context.SaveChangesAsync();
	}
	public async Task UpdateAsync(T entity)
	{
		_dbSet.Update(entity);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(T entity)
	{
		_dbSet.Remove(entity);
		await _context.SaveChangesAsync();
	}
	public async Task<ICollection<T>> GetAllAsync() => await _dbSet.ToListAsync();

	public async Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
	{
		var query = _dbSet.AsQueryable();

		includes.ToList().ForEach(i => query = query.Include(i));

		return await query.ToListAsync();
	}

	public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

	public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
	{
		var query = _dbSet.AsQueryable();

		includes.ToList().ForEach(i => query.Include(i));

		return await query.SingleOrDefaultAsync(e => e.Id == id);
	}

	public async Task<T?> GetBy(Expression<Func<T, bool>> predicate)
		=> await _dbSet.FirstOrDefaultAsync(predicate);

	public async Task<bool> ExistsBy(Expression<Func<T, bool>> predicate)
		=> await _dbSet.AnyAsync(predicate);

	public async Task<ICollection<T>> GetListBy(Expression<Func<T, bool>> predicate)
		=> await _dbSet.Where(predicate).ToListAsync();

	public async Task<ICollection<T>> GetListBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
	{
		var query = _dbSet.AsQueryable();

		includes.ToList().ForEach(i => query = query.Include(i));

		return await query.Where(predicate).ToListAsync();
	}
}
