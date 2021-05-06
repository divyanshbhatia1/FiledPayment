using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FiledPayment.Application.Contracts.Persistence
{
	public interface IAsyncRepository<T> where T : class
	{
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
