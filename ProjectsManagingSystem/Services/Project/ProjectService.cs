using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;
using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Models.Project;
using ProjectsManagingSystem.Models.ProjectTask;

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

    public bool AddMemberToProject(int id, int memberId)
    {
        var project = _dbContext
            .Projects
            .Include(m => m.Members)
            .FirstOrDefault(x => x.Id == id);

        var member = _dbContext
            .Members
            .Include(m => m.Projects)
            .FirstOrDefault(x => x.Id == memberId);

        if (project == null || member == null) { return false; }
        
        member.Projects.Add(project);
        project.Members.Add(member);

        _dbContext.SaveChanges();

        return true;

    }


    public ProjectTaskResponseDto AddTaskToProject(int id, ProjectTaskDto dto)
    {
        dto.ProjectId = id;
        dto.MemberId = 1;

        var task = _mapper.Map<Entities.ProjectTask>(dto);

        _dbContext.ProjectTasks.Add(task);
        _dbContext.SaveChanges();

        var taskResponse = _mapper.Map<ProjectTaskResponseDto>(task);
        return taskResponse;

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
        var project = _dbContext.Projects.Include(m => m.Members).FirstOrDefault(x => x.Id == id);
        var members = project?.Members;
        
        var result = _mapper.Map<List<MemberResponseDto>>(members);
        return result;
    }

    public IEnumerable<ProjectTaskResponseDto> GetTasks(int id)
    {
        var project = _dbContext
            .Projects
            .Include(t => t.Tasks).FirstOrDefault(x => x.Id == id);

        var tasks = project?.Tasks;

        var result = _mapper.Map<List<ProjectTaskResponseDto>>(tasks);

        return result;
    }

    public bool AssignMemberToTask(int projectId, int taskId, int memberId)
    {
        var project = _dbContext.Projects
            .Include(r => r.Tasks)
            .Include(m => m.Members)
            .FirstOrDefault(x => x.Id == projectId);
        
        var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task is null) return false;

        var member = project.Members.FirstOrDefault(m => m.Id == memberId);
        if (member is null) return false;

        task.MemberId = member.Id;

        var response = _mapper.Map<ProjectTaskResponseDto>(task);
        _dbContext.SaveChanges();
        return true;
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