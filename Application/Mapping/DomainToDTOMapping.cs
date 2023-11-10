using AutoMapper;
using LearnApi.Domain.Models;

namespace LearnApi.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}