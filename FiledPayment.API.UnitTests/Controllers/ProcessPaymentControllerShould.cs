using FiledPayment.API.Controllers;
using FiledPayment.Application.Contracts.Services;
using FiledPayment.Domain.DTO.Request;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace FiledPayment.API.UnitTests.Controllers
{
	public class ProcessPaymentControllerShould
	{
		private readonly ProcessPaymentController _processPaymentController;
		private readonly Mock<IPaymentService> _mockPaymentService;

		public ProcessPaymentControllerShould()
		{
			_mockPaymentService = new Mock<IPaymentService>();
			_processPaymentController = new ProcessPaymentController(_mockPaymentService.Object);
		}

		[Theory]
		[MemberData(nameof(PaymentRequestTestData))]
		public void GivenPaymentRequest_WhenValidateRequest_ThenValidateRequest(PaymentRequest request, bool expected)
		{
			var validationResults = new List<ValidationResult>();
			var actual = Validator.TryValidateObject(request, new ValidationContext(request), validationResults, true);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public async Task GivenPaymentRequest_WhenRequestProcessPayment_ThenProcessPayment()
		{
			await _processPaymentController.ProcessPayment(new PaymentRequest());
			_mockPaymentService.Verify(mock => mock.ProcessPayment(It.IsAny<PaymentRequest>()), Times.Once);
		}

		public static IEnumerable<object[]> PaymentRequestTestData =>
			new List<object[]>
			{
				new object[] {new PaymentRequest(), false},
				new object[] {new PaymentRequest
				{
					Amount = -1, //Negative amount
					CardHolder = "Divyansh",
					ExpirationDate = DateTime.Now.AddDays(1),
					CreditCardNumber = "1234123412341234",
					SecurityCode = "123"
				}, false },
				new object[] {new PaymentRequest
				{
					Amount = 1,
					CardHolder = "Divyansh",
					ExpirationDate = DateTime.Now.AddDays(-1), //Expired Card
					CreditCardNumber = "1234123412341234",
					SecurityCode = "123"
				}, false },
				new object[] {new PaymentRequest //No CreditCard Number
				{
					Amount = 1,
					CardHolder = "Divyansh",
					ExpirationDate = DateTime.Now.AddDays(-1),
					SecurityCode = "123"
				}, false },
				new object[] {new PaymentRequest //No Card Holder
				{
					Amount = 1,
					ExpirationDate = DateTime.Now.AddDays(-1),
					CreditCardNumber = "1234123412341234",
					SecurityCode = "123"
				}, false },
				new object[] {new PaymentRequest
				{
					Amount = 1,
					CardHolder = "Divyansh",
					ExpirationDate = DateTime.Now.AddDays(-1),
					CreditCardNumber = "1234123412341234",
					SecurityCode = "1234" //Security Number 4 digits
				}, false },
				new object[] {new PaymentRequest
				{
					Amount = 1,
					CardHolder = "Divyansh",
					ExpirationDate = DateTime.Now.AddDays(1),
					CreditCardNumber = "4111111111111111",
					SecurityCode = "123"
				}, true }
			};
	}
}
