using AutoMapper;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;
using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Models.Project;
using ProjectsManagingSystem.Models.ProjectTask;

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