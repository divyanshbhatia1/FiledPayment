using FiledPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace FiledPayment.Persistence.Configurations
{
	public class PaymentStateConfiguration : IEntityTypeConfiguration<PaymentState>
	{
		public void Configure(EntityTypeBuilder<PaymentState> builder)
		{
			builder.HasData(GetPaymentStates());
		}

		private List<PaymentState> GetPaymentStates()
		{
			List<PaymentState> paymentStates = new List<PaymentState>();
			foreach (Domain.Enums.PaymentState paymentState in Enum.GetValues(typeof(Domain.Enums.PaymentState)))
			{
				paymentStates.Add(new PaymentState { Id = (int)paymentState, Name = paymentState.ToString() });
			}
			return paymentStates;
		}
	}
}
