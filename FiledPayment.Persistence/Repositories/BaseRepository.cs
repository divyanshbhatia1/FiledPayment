using FiledPayment.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiledPayment.Persistence.Repositories
{
	public class BaseRepository<T> : IAsyncRepository<T> where T : class
	{
		protected readonly FiledPaymentDbContext _dbContext;

		public BaseRepository(FiledPaymentDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
		}

		public virtual async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> ListAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public void Update(T entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
		}
	}
}
