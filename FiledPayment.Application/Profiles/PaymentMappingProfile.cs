using AutoMapper;
using FiledPayment.Domain.DTO.Request;
using FiledPayment.Domain.Entities;

namespace FiledPayment.Application.Profiles
{
	public class PaymentMappingProfile : Profile
	{
		public PaymentMappingProfile()
		{
			CreateMap<PaymentRequest, Payment>();
		}
	}
}
