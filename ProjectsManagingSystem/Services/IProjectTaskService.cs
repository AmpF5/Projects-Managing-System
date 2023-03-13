using ProjectsManagingSystem.Entities;

namespace ProjectsManagingSystem.Services
{
    public interface IProjectTaskService
    {
        IEnumerable<ProjectTask> GetAll();
        ProjectTask GetTask(int id);
    }
}
