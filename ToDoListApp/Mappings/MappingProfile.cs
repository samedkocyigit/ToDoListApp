using AutoMapper;
using ToDoListApp.Domain.Models.DTOs;
using ToDoListApp.Domain.Models.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserRegisterDto, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<CreateTaskDto, Tasks>();  
        CreateMap<Tasks, TaskDto>();    
    }
}
