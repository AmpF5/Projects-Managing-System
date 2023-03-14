using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Models;

namespace ProjectsManagingSystem.Services.Project;

public interface IProjectService
{
    ProjectResponseDto Create(ProjectDto dto);
    ProjectResponseDto GetById(int id);
    bool Delete(int id);
    bool Update(ProjectDto dto, int id);
}