using AutoMapper;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;

namespace ProjectsManagingSystem.Mappers;

public class ProjectMapperProfile : Profile
{
    public ProjectMapperProfile()
    {
        CreateMap<Project, ProjectResponseDto>();
        CreateMap<ProjectDto, Project>();
        CreateMap<ProjectTask, ProjectTaskResponseDto>();
        CreateMap<ProjectTaskDto, ProjectTask>();
        CreateMap<Member, MemberResponseDto>();
        CreateMap<MemberDto, Member>();
    }
}