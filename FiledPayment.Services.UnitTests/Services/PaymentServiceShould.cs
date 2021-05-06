using AutoMapper;
using FiledPayment.Application.Contracts.External;
using FiledPayment.Application.Contracts.Persistence;
using FiledPayment.Application.Profiles;
using FiledPayment.Domain.DTO.Request;
using FiledPayment.Domain.Entities;
using FiledPayment.Persistence;
using FiledPayment.Persistence.Repositories;
using FiledPayment.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FiledPayment.Services.UnitTests.Services
{
	public class PaymentServiceShould
	{
		private readonly IMapper _mapper;
		private readonly PaymentService _paymentService;
		private readonly BaseRepository<Payment> _paymentRepository;
		private readonly BaseRepository<PaymentState> _paymentStateRepository;
		private readonly UnitOfWork _unitOfWork;
		private readonly Mock<IExpensivePaymentGateway> _mockExpensivePaymentGateway;
		private readonly Mock<ICheapPaymentGateway> _mockCheapPaymentGateway;
		private readonly FiledPaymentDbContext _dbContext;

		public PaymentServiceShould()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AllowNullCollections = true;
				cfg.AddProfile<PaymentMappingProfile>();
			});

			_mapper = config.CreateMapper();
			_dbContext = GetContextWithData();
			_paymentRepository = new BaseRepository<Payment>(_dbContext);
			_paymentStateRepository = new BaseRepository<PaymentState>(_dbContext);
			_unitOfWork = new UnitOfWork(_dbContext, _paymentRepository, _paymentStateRepository);
			_mockExpensivePaymentGateway = new Mock<IExpensivePaymentGateway>();
			_mockCheapPaymentGateway = new Mock<ICheapPaymentGateway>();
			_paymentService = new PaymentService(_mapper, _unitOfWork, _mockExpensivePaymentGateway.Object, _mockCheapPaymentGateway.Object);
		}

		[Theory]
		[InlineData(10)]
		public async Task GivenRequestWithAmountLessThan20_WhenProcessRequest_ThenProcessWithCheapGateway(decimal amount)
		{
			_mockCheapPaymentGateway.Setup(x => x.Process(It.IsAny<PaymentRequest>())).ReturnsAsync(1);
			var request = new PaymentRequest { Amount = amount };
			await _paymentService.ProcessPayment(request);
			_mockCheapPaymentGateway.Verify(x => x.Process(It.IsAny<PaymentRequest>()), Times.Once);
		}

		[Theory]
		[InlineData(30)]
		public async Task GivenRequestWithAmountBetween20And500_WhenProcessRequestAndExpensivePaymentGatewayAvailable_ThenProcessWithExpensiveGateway(decimal amount)
		{
			_mockExpensivePaymentGateway.Setup(x => x.Process(It.IsAny<PaymentRequest>())).ReturnsAsync(1);
			_mockExpensivePaymentGateway.Setup(x => x.IsAvailable()).Returns(true);
			var request = new PaymentRequest { Amount = amount };
			await _paymentService.ProcessPayment(request);
			_mockExpensivePaymentGateway.Verify(x => x.Process(It.IsAny<PaymentRequest>()), Times.Once);
		}

		[Theory]
		[InlineData(30)]
		public async Task GivenRequestWithAmountBetween20And500_WhenProcessRequestAndExpensivePaymentGatewayNotAvailable_ThenProcessWithExpensiveGateway(decimal amount)
		{
			_mockCheapPaymentGateway.Setup(x => x.Process(It.IsAny<PaymentRequest>())).ReturnsAsync(1);
			_mockExpensivePaymentGateway.Setup(x => x.IsAvailable()).Returns(false);
			var request = new PaymentRequest { Amount = amount };
			await _paymentService.ProcessPayment(request);
			_mockCheapPaymentGateway.Verify(x => x.Process(It.IsAny<PaymentRequest>()), Times.Once);
		}

		[Theory]
		[InlineData(510)]
		public async Task GivenRequestWithAmountMoreThan500_WhenProcessRequest_ThenProcessWithExpensiveGateway(decimal amount)
		{
			_mockExpensivePaymentGateway.Setup(x => x.Process(It.IsAny<PaymentRequest>())).ReturnsAsync(1);
			var request = new PaymentRequest { Amount = amount };
			await _paymentService.ProcessPayment(request);
			_mockExpensivePaymentGateway.Verify(x => x.Process(It.IsAny<PaymentRequest>()), Times.Once);
		}

		[Theory]
		[InlineData(510)]
		public async Task GivenRequestWithAmountMoreThan500_WhenIssueWithProcessRequest_ThenProcess3Times(decimal amount)
		{
			_mockExpensivePaymentGateway.Setup(x => x.Process(It.IsAny<PaymentRequest>())).Throws<Exception>();
			var request = new PaymentRequest { Amount = amount };
			await _paymentService.ProcessPayment(request);
			_mockExpensivePaymentGateway.Verify(x => x.Process(It.IsAny<PaymentRequest>()), Times.Exactly(4));
		}

		private FiledPaymentDbContext GetContextWithData()
		{
			var options = new DbContextOptionsBuilder<FiledPaymentDbContext>()
					  .UseInMemoryDatabase(Guid.NewGuid().ToString())
					  .Options;

			var context = new FiledPaymentDbContext(options);

			context.PaymentStates.Add(new PaymentState { Id = 1, Name = "Processed" });

			return context;
		}
	}
}
