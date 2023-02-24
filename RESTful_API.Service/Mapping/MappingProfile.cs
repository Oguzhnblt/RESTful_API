using AutoMapper;
using RESTful_API.DTO.Entities;
using RESTful_API.DTO.Enum;
using RESTful_API.DTO.Models;

namespace RESTful_API.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Book, BookDTO>().ForMember(x => x.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.ID).ToString()));

            CreateMap<Genre, GenreDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();


        }

    }
}
