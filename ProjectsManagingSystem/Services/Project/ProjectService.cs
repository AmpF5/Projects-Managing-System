using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;

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
    public IActionResult Create(ProjectDto dto)
    {
        throw new NotImplementedException();
    }

    public ProjectResponseDto GetById(int id)
    {
        var project = _dbContext.Projects
            // .Include(r => r.Members)
            .Include(r => r.Tasks)
            .FirstOrDefault(x => x.Id == id);
        // project.Members = _dbContext.Members.Where(x => x.Projects.Any(p => p.Id == id)).ToList();
        // project.Tasks = _dbContext.ProjectTasks.Where(x => x.ProjectId == id).ToList();
        var result = _mapper.Map<ProjectResponseDto>(project);
        return result;
    }

    public IActionResult Delete(int id)
    {
        throw new NotImplementedException();
    }
}