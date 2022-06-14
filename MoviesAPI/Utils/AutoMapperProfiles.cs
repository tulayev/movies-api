using AutoMapper;
using MoviesAPI.DTO;
using MoviesAPI.Models;

namespace MoviesAPI.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Movie, MovieDTO>()
                .ForMember(dto => dto.Cinemas, model => model.MapFrom(p => p.CinemaHalls.Select(c => c.Cinema)))
                .ForMember(dto => dto.Actors, model => model.MapFrom(p => p.ActorsMovies.Select(am => am.Actor)));
        }
    }
}
