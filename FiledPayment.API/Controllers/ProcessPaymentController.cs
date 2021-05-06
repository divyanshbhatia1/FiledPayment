using FiledPayment.Application.Contracts.Services;
using FiledPayment.Domain.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FiledPayment.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProcessPaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;

		public ProcessPaymentController(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		/// <summary>
		/// Process Payment
		/// </summary>
		/// <param name="paymentRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> ProcessPayment(PaymentRequest paymentRequest)
		{
			await _paymentService.ProcessPayment(paymentRequest);
			return Ok("Payment Processed successfully");
		}
	}
}
