using AutoMapper;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Store, StoreDto>().ReverseMap();
        }
    }
}
