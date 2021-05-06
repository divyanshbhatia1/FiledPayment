using FiledPayment.Application.Contracts.Persistence;
using FiledPayment.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FiledPayment.Persistence
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<FiledPaymentDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

			services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

			services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

			return services;
		}
	}
}
