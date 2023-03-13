using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;
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
        
        return Ok(request);
    }

    [HttpGet]
    public IActionResult GetProject(int id)
    {
        var project = _projectService.GetById(id);
        // TODO: check if this is a correct way to approach returning whole project
        // var membersOfProject = _membersService.GetById(project.Id);
        // var tasksInProject = _ProjectTasks.GetById(project.Id);
        return Ok(project);
    }
}