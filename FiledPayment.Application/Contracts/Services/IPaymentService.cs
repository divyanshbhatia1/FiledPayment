using FiledPayment.Domain.DTO.Request;
using System.Threading.Tasks;

namespace FiledPayment.Application.Contracts.Services
{
	public interface IPaymentService
	{
		Task ProcessPayment(PaymentRequest paymentRequest);
	}
}
