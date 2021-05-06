using AutoMapper;
using FiledPayment.Application.Contracts.External;
using FiledPayment.Application.Contracts.Persistence;
using FiledPayment.Application.Contracts.Services;
using FiledPayment.Domain.DTO.Request;
using FiledPayment.Domain.Entities;
using Polly;
using System;
using System.Threading.Tasks;

namespace FiledPayment.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IExpensivePaymentGateway _expensivePaymentGateway;
		private readonly ICheapPaymentGateway _cheapPaymentGateway;
		private const int RETRY_COUNT = 3;

		public PaymentService(IMapper mapper, IUnitOfWork unitOfWork, IExpensivePaymentGateway expensivePaymentGateway, ICheapPaymentGateway cheapPaymentGateway)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_expensivePaymentGateway = expensivePaymentGateway;
			_cheapPaymentGateway = cheapPaymentGateway;
		}

		public async Task ProcessPayment(PaymentRequest paymentRequest)
		{
			var payment = _mapper.Map<Payment>(paymentRequest);

			try
			{
				payment.PaymentStateId = await ProcessPaymentInternal(paymentRequest);
			}
			catch (Exception)
			{
				payment.PaymentStateId = (int)Domain.Enums.PaymentState.Failed;
			}

			_unitOfWork.Payments.Add(payment);

			await _unitOfWork.CommitAsync();
		}

		private async Task<int> ProcessPaymentInternal(PaymentRequest paymentRequest)
		{
			if(paymentRequest.Amount <= 20)
			{
				return await _cheapPaymentGateway.Process(paymentRequest);
				
			}

			if(paymentRequest.Amount >= 21 && paymentRequest.Amount <= 500)
			{
				if(_expensivePaymentGateway.IsAvailable())
				{
					return await _expensivePaymentGateway.Process(paymentRequest);
				}

				return await _cheapPaymentGateway.Process(paymentRequest);
			}

			var retryPolicy = Policy
				.Handle<Exception>()
				.RetryAsync(RETRY_COUNT);

			return await retryPolicy.ExecuteAsync(async () =>
			{
				return await _expensivePaymentGateway.Process(paymentRequest);
			});
		}
	}
}
