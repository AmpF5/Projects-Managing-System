using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Models;

namespace ProjectsManagingSystem.Services.Project;

public interface IProjectService
{
    IActionResult Create(ProjectDto dto);
    ProjectResponseDto GetById(int id);
    IActionResult Delete(int id);
}