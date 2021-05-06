using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiledPayment.Domain.DTO.Request
{
	public class PaymentRequest : IValidatableObject
	{
		[Required]
		[CreditCard]
		public string CreditCardNumber { get; set; }

		[Required]
		public string CardHolder { get; set; }

		[Required]
		public DateTime ExpirationDate { get; set; }

		[StringLength(3)]
		public string SecurityCode { get; set; }

		[Required]
		public decimal Amount { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Amount < 0)
				yield return new ValidationResult("Amount should be a positive.");

			if (ExpirationDate < DateTime.Now)
				yield return new ValidationResult("Card expired");
		}
	}
}
