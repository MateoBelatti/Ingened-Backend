using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Core.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        // Mapeo bidireccional entre la entidad User y sus DTOs
        CreateMap<User, UserCreateDTO>().ReverseMap();
        CreateMap<User, UserUpdateDTO>().ReverseMap();
        CreateMap<User, RegisterDTO>().ReverseMap();
        CreateMap<User, LoginDTO>().ReverseMap();
        CreateMap<User, GoogleLoginDTO>().ReverseMap();
        
        CreateMap<User, UserResponseDTO>().ReverseMap();
    }
}
