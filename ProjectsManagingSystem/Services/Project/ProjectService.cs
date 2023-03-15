using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;
using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Models.Project;

namespace ProjectsManagingSystem.Services.Project;

public class ProjectService : IProjectService
{
    private readonly ProjectSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProjectService(ProjectSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public ProjectResponseDto Create(ProjectDto dto)
    {
        var project = _mapper.Map<Entities.Project>(dto);
        
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
        var projectResponse = _mapper.Map<ProjectResponseDto>(project);
        return projectResponse;
    }

    public ProjectResponseDto GetById(int id)
    {
        var project = _dbContext.Projects
            .Include(r => r.Tasks)
            .Include(m => m.Members)
            .FirstOrDefault(x => x.Id == id);
        
        var result = _mapper.Map<ProjectResponseDto>(project);
        return result;
    }

    public IEnumerable<MemberResponseDto> GetMembers(int id)
    {
        // var members = _dbContext.Members.Where(i => i.Id == id).ToList();
        // var members = _dbContext.Members.Select(m => m.Projects.Where(i => i.Id == id));
        
        // var members = _dbContext.Members.Select(member => member.Projects).Select(i => i.Where(x => x.Id == id)).ToList();
        var project = _dbContext.Projects.Include(m => m.Members).FirstOrDefault(x => x.Id == id);
        var members = project?.Members;
        
        var result = _mapper.Map<List<MemberResponseDto>>(members);
        return result;
    }

    public bool Delete(int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(i => i.Id == id);
        
        if (project is null) return false;
        _dbContext.Projects.Remove(project);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(ProjectDto dto, int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(i => i.Id == id);
        
        if (project is null) return false;
        project.Name = dto.Name;
        project.Description = dto.Description;
        project.Deadline = dto.Deadline;

        _dbContext.SaveChanges();
        return true;

    }
}