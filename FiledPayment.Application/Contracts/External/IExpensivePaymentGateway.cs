using FiledPayment.Domain.DTO.Request;
using System.Threading.Tasks;

namespace FiledPayment.Application.Contracts.External
{
	public interface IExpensivePaymentGateway
	{
		Task<int> Process(PaymentRequest paymentRequest);
		bool IsAvailable();
	}
}
