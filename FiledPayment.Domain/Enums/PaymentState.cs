using System.Runtime.Serialization;

namespace FiledPayment.Domain.Enums
{
	public enum PaymentState
	{
		[EnumMember(Value = "Pending")]
		Pending = 1,
		[EnumMember(Value = "Processed")]
		Processed,
		[EnumMember(Value = "Failed")]
		Failed
	}
}
