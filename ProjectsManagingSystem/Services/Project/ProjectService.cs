using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.ExtensionMethods;
using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Models.Project;
using ProjectsManagingSystem.Models.ProjectTask;
using ProjectsManagingSystem.Services.Member;

namespace ProjectsManagingSystem.Services.Project;

public class ProjectService : IProjectService
{
    private readonly ProjectSystemDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMemberService _memberService;

    public ProjectService(ProjectSystemDbContext dbContext, IMapper mapper, IMemberService memberService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _memberService = memberService;
    }
    public ProjectResponseDto Create(ProjectDto dto)
    {
        var project = _mapper.Map<Entities.Project>(dto);
        
        var memberId = _memberService.GetMemberIdFromJwt();
        
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
        var member = _dbContext.MemberProjects.Add(
            new MemberProject()
            {
                MemberId = memberId,
                ProjectId = project.Id,
                Role = Role.Moderator
            });
        _dbContext.SaveChanges();
        var projectResponse = _mapper.Map<ProjectResponseDto>(project);
        return projectResponse;
    }

    public bool AddMemberToProject(int id, int memberId)
    {
        if (!_memberService.AuthorizeModerator(id)) return false;
        
        var project = _dbContext
            .Projects
            .IncludeMembers()
            .FirstOrDefault(x => x.Id == id);

        var member = _dbContext
            .Members
            .IncludeProjects()
            .FirstOrDefault(x => x.Id == memberId);

        if (project == null || member == null) { return false; }
        
        project.MemberProjects.Add(
            new MemberProject()
            {
                MemberId = memberId,
                ProjectId = id,
                Role = Role.Member
            });

        _dbContext.SaveChanges();
        return true;
    }

    public ProjectTaskResponseDto AddTaskToProject(int id, ProjectTaskDto dto)
    {
        if (!_memberService.AuthorizeMemberInProject(id)) return null;
        
        dto.ProjectId = id;
        var task = _mapper.Map<Entities.ProjectTask>(dto);
        var taskResponse = _mapper.Map<ProjectTaskResponseDto>(task);

        _dbContext.ProjectTasks.Add(task);
        _dbContext.SaveChanges();
        return taskResponse;
    }

    public ProjectResponseDto GetById(int id)
    {
        if (!_memberService.AuthorizeMemberInProject(id)) return null;

        var project = _dbContext.Projects
            .IncludeMembers()
            .Include(r => r.Tasks)
            .FirstOrDefault(x => x.Id == id);
        
        var result = _mapper.Map<ProjectResponseDto>(project);
        return result;
    }

    public IEnumerable<MemberResponseDto> GetMembers(int id)
    {
        if (!_memberService.AuthorizeMemberInProject(id)) return null;
        
        var project = _dbContext.Projects
            .IncludeMembers()
            .FirstOrDefault(x => x.Id == id);

        var members = project.MemberProjects.Select(x => x.Member).ToList();
        var result = _mapper.Map<List<MemberResponseDto>>(members);
        return result;
    }

    public IEnumerable<ProjectTaskResponseDto> GetTasks(int id)
    {
        if (!_memberService.AuthorizeMemberInProject(id)) return null;

        var project = _dbContext
            .Projects
            .Include(t => t.Tasks).FirstOrDefault(x => x.Id == id);

        var tasks = project?.Tasks;

        var result = _mapper.Map<List<ProjectTaskResponseDto>>(tasks);

        return result;
    }

    public IEnumerable<ProjectTaskResponseDto> GetMemberProjectTask(int id, int memberId)
    {
        if (!_memberService.AuthorizeMemberInProject(id)) return null;

        var tasks = _dbContext.Projects
            .Where(p => p.Id == id)
            .SelectMany(p => p.Tasks)
            .Where(t => t.MemberId == memberId);

        if (tasks == null)
        {
            return new List<ProjectTaskResponseDto>();
        }

        var result = _mapper.Map<List<ProjectTaskResponseDto>>(tasks);
        return result;
    }

    public bool AssignMemberToTask(int projectId, int taskId, int memberId)
    {
        if (!_memberService.AuthorizeMemberInProject(projectId)) return false;

        var project = _dbContext.Projects
            .IncludeMembers()
            .Include(r => r.Tasks)
            .FirstOrDefault(x => x.Id == projectId);
        if (project is null) return false;
        
        var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task is null) return false;

        var member = project.MemberProjects.FirstOrDefault(m => m.MemberId == memberId);
        if (member is null) return false;

        task.MemberId = memberId;

        var response = _mapper.Map<ProjectTaskResponseDto>(task);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        if (!_memberService.AuthorizeModerator(id)) return false;

        var project = _dbContext.Projects.FirstOrDefault(i => i.Id == id);
        
        if (project is null) return false;
        _dbContext.Projects.Remove(project);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(ProjectDto dto, int id)
    {
        if (!_memberService.AuthorizeModerator(id)) return false;

        var project = _dbContext.Projects.FirstOrDefault(i => i.Id == id);
        
        if (project is null) return false;
        project.Name = dto.Name;
        project.Description = dto.Description;
        project.Deadline = dto.Deadline;

        _dbContext.SaveChanges();
        return true;
    }
}