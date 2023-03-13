using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;

namespace ProjectsManagingSystem.Services.Task
{
    public interface ITaskService
    {
        ProjectTaskResponseDto GetById(int id);
        IEnumerable<ProjectTaskResponseDto> GetAll();
        public bool Update(ProjectTaskDto dto, int id);
        public int Create(ProjectTaskDto dto);
        public bool Delete(int id);
    }
}
