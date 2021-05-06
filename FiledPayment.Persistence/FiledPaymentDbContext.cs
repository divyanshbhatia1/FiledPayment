using FiledPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiledPayment.Persistence
{
	public class FiledPaymentDbContext : DbContext
	{
		public FiledPaymentDbContext(DbContextOptions<FiledPaymentDbContext> options)
			: base(options)
		{ }

		public DbSet<Payment> Payments { get; set; }
		public DbSet<PaymentState> PaymentStates { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(FiledPaymentDbContext).Assembly);
		}
	}
}
