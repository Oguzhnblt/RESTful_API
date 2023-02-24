using AutoMapper;
using RESTful_API.DTO.Entities;
using RESTful_API.DTO.Models;

namespace RESTful_API.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BooküDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();

        }

    }
}
