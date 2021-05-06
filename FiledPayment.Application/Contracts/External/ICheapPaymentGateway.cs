using FiledPayment.Domain.DTO.Request;
using System.Threading.Tasks;

namespace FiledPayment.Application.Contracts.External
{
	public interface ICheapPaymentGateway
	{
		Task<int> Process(PaymentRequest paymentRequest);
	}
}
