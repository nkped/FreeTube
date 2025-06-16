using AutoMapper;
using FreeTube.Models;
namespace FreeTube.Dtos
{
    public class GenreProfile : Profile
    {
        public GenreProfile() 
        { 
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
        }

    }
}
