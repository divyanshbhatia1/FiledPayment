using FiledPayment.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FiledPayment.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
	{
		IAsyncRepository<Payment> Payments { get; }
		IAsyncRepository<PaymentState> PaymentStates { get; }
		Task<int> CommitAsync();
	}
}
