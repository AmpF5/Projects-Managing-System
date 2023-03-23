using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Models.Project;
using ProjectsManagingSystem.Models.ProjectTask;
using ProjectsManagingSystem.Services.Project;

namespace ProjectsManagingSystem.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    
    [HttpPost]
    public IActionResult CreateProject([FromBody] ProjectDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var project = _projectService.Create(request);
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    [HttpPost("{projectId:int}/task")]
    public ActionResult AddTaskToProject([FromRoute] int projectId, [FromBody] ProjectTaskDto dto )
    {
        var task = _projectService.AddTaskToProject(projectId, dto);
        return CreatedAtAction(nameof(GetProject), new { id = task.Id }, task);
    }

    [HttpGet("{projectId:int}")]
    public IActionResult GetProject(int projectId)
    {
        var project = _projectService.GetById(projectId);
        return Ok(project);
    }

    [HttpGet("{projectId:int}/members")]
    public ActionResult<IEnumerable<MemberResponseDto>> GetMembers(int projectId)
    {
        var members = _projectService.GetMembers(projectId);
        return members.IsNullOrEmpty() ? NoContent() : Ok(members);
    }

    [HttpGet("{projectId:int}/member/{memberId:int}/tasks")]
    public ActionResult<IEnumerable<MemberResponseDto>> GetMemberProjectTask(int projectId, int memberId)
    {
        var members = _projectService.GetMemberProjectTask(projectId,memberId);
        return members.IsNullOrEmpty() ? NoContent() : Ok(members);
    }

    [HttpGet("{projectId:int}/tasks")]
    public ActionResult<IEnumerable<ProjectTaskResponseDto>> GetAllTasks(int projectId)
    {
        var tasks = _projectService.GetTasks(projectId);
        return tasks.IsNullOrEmpty() ? NoContent() : Ok(tasks);
    }
    
    [HttpPut("{projectId:int}/task/{taskId:int}/member/{memberId:int}")]
    public IActionResult AssignMemberToTask([FromRoute] int projectId,[FromRoute] int taskId,[FromRoute] int memberId)
    {
        var isTaskValid = _projectService.AssignMemberToTask(projectId, taskId, memberId);
        return isTaskValid ? NoContent() : NotFound();
    }

    [HttpDelete("{projectId:int}")]
    public IActionResult DeleteProject([FromRoute] int projectId)
    {
        var isDeleted = _projectService.Delete(projectId);
        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPut("{projectId:int}")]
    public IActionResult UpdateProject([FromBody] ProjectDto dto, [FromRoute] int projectId)
    {
        if (!ModelState.IsValid) return BadRequest();
        var project = _projectService.Update(dto, projectId);
        return project ? Ok() : NotFound();
    }

    [HttpPut("{projectId:int}/member/{memberId:int}")]
    public IActionResult AddMemberToProject([FromRoute] int projectId, [FromRoute] int memberId)
    {
        var project = _projectService.AddMemberToProject(projectId, memberId);
        return project ? Ok() : NotFound();
    }
}