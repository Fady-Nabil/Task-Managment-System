using AutoMapper;
using TaskMangmentSystem.API.Dtos.EmployeeDto;
using TaskMangmentSystem.API.Dtos.IssueDto;
using TaskMangmentSystem.Core.Entities;

namespace TaskMangmentSystem.API.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // source , destination
            CreateMap<Issue, GetIssueDto>()
            .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Assignee.Name))
            .ForMember(d => d.EmployeeId, o => o.MapFrom(s => s.Assignee.Id))
            .ForMember(d => d.StatusName, o => o.MapFrom(s => s.Status.ToString()))
            .ReverseMap();

            CreateMap<Employee, GetEmployeeDto>()
            .ReverseMap();
        }
    }
}
