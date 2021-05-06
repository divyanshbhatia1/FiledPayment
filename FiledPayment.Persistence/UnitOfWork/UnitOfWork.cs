using FiledPayment.Application.Contracts.Persistence;
using FiledPayment.Domain.Entities;
using FiledPayment.Persistence.Repositories;
using System.Threading.Tasks;

namespace FiledPayment.Persistence.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly FiledPaymentDbContext _dbContext;

		public UnitOfWork(FiledPaymentDbContext dbContext, 
			IAsyncRepository<Payment> paymentRepository, 
			IAsyncRepository<PaymentState> paymentStateRepository)
		{
			_dbContext = dbContext;
			Payments = paymentRepository;
			PaymentStates = paymentStateRepository;
		}

		public IAsyncRepository<Payment> Payments { get; }

		public IAsyncRepository<PaymentState> PaymentStates { get; }

		public async Task<int> CommitAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public void Dispose()
		{
			_dbContext?.Dispose();
		}
	}
}
