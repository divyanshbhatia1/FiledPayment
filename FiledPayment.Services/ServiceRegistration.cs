using FiledPayment.Application.Contracts.External;
using FiledPayment.Application.Contracts.Services;
using FiledPayment.Services.External;
using Microsoft.Extensions.DependencyInjection;

namespace FiledPayment.Services
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IPaymentService, PaymentService>();
			services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
			services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>();

			return services;
		}
	}
}
