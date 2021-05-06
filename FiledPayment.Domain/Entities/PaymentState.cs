using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiledPayment.Domain.Entities
{
	public class PaymentState
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(50)")]
		public string Name { get; set; }
	}
}
