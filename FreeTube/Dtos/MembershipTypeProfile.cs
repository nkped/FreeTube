using AutoMapper;
using FreeTube.Models;

namespace FreeTube.Dtos
{
    public class MembershipTypeProfile : Profile
    {
        public MembershipTypeProfile()
        {
            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<MembershipTypeDto, MembershipType>();
        }
    }
}