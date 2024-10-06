using AutoMapper;
using ToDoListApp.DTOs;
using ToDoListApp.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTaskDto, Tasks>();  
        CreateMap<Tasks, TaskDto>();    
    }
}
