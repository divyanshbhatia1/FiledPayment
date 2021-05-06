using FiledPayment.Application.Contracts.External;
using FiledPayment.Domain.DTO.Request;
using FiledPayment.Domain.Enums;
using System.Threading.Tasks;

namespace FiledPayment.Services.External
{
	public class CheapPaymentGateway : ICheapPaymentGateway
	{
		public async Task<int> Process(PaymentRequest paymentRequest)
		{
			return await Task.FromResult((int)PaymentState.Processed); //Returning Processed Status for now
		}
	}
}
