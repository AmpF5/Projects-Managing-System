using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;
using ProjectsManagingSystem.Models.Project;
using ProjectsManagingSystem.Services.Project;

namespace ProjectsManagingSystem.Controllers;
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
        return Ok(project);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetProject(int id)
    {
        var project = _projectService.GetById(id);
        // TODO: check if this is a correct way to approach returning whole project
        // var membersOfProject = _membersService.GetById(project.Id);
        // var tasksInProject = _ProjectTasks.GetById(project.Id);
        return Ok(project);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var isDeleted = _projectService.Delete(id);
        
        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPut("{id:int}")]
    public IActionResult Put([FromBody] ProjectDto dto, [FromRoute] int id)
    {
        if (!ModelState.IsValid) return BadRequest();

        var project = _projectService.Update(dto, id);

        return project ? Ok() : NotFound();
    }
}