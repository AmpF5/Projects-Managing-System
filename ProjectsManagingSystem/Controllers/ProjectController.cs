﻿using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;
using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Models.Project;
using ProjectsManagingSystem.Models.ProjectTask;
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
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    [HttpPost("{id:int}/task")]
    public ActionResult AddTaskToProject([FromRoute] int id, [FromBody] ProjectTaskDto dto )
    {

        var task = _projectService.AddTaskToProject(id, dto);
        return CreatedAtAction(nameof(GetProject), new { id = task.Id }, task);

    }





    [HttpGet("{id:int}")]
    public IActionResult GetProject(int id)
    {
        var project = _projectService.GetById(id);
        return Ok(project);
    }

    [HttpGet("{id:int}/members")]
    public ActionResult<IEnumerable<MemberResponseDto>> GetMembers(int id)
    {
        var members = _projectService.GetMembers(id);

        return members.IsNullOrEmpty() ? NoContent() : Ok(members);
        // return Ok(members);
    }

    [HttpGet("{id:int}/tasks")]
    public ActionResult<IEnumerable<ProjectTaskResponseDto>> GetAllTasks(int id)
    {
        var tasks = _projectService.GetTasks(id);

        return tasks.IsNullOrEmpty() ? NoContent() : Ok(tasks);
    }


    [HttpDelete("{id:int}")]
    public IActionResult DeleteProject([FromRoute] int id)
    {
        var isDeleted = _projectService.Delete(id);
        
        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateProject([FromBody] ProjectDto dto, [FromRoute] int id)
    {
        if (!ModelState.IsValid) return BadRequest();

        var project = _projectService.Update(dto, id);

        return project ? Ok() : NotFound();
    }
}