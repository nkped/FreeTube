using AutoMapper;
using FreeTube.Models;
namespace FreeTube.Dtos
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();            
            CreateMap<CustomerDto, Customer>();            
        }
    }
}
