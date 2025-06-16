using AutoMapper;
using FreeTube.Models;
namespace FreeTube.Dtos
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();
        }
    }
}
