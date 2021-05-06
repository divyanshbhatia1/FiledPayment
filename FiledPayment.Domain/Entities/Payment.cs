using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiledPayment.Domain.Entities
{
	public class Payment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(20)")]
		public string CreditCardNumber { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(50)")]
		public string CardHolder { get; set; }

		[Required]
		public DateTime ExpirationDate { get; set; }

		[Column(TypeName = "VARCHAR(3)")]
		public string SecurityCode { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(18,6)")]
		public decimal Amount { get; set; }

		public int PaymentStateId { get; set; }

		[ForeignKey("PaymentStateId")]
		public virtual PaymentState PaymentState { get; set; }
	}
}
