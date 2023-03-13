using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Services;

namespace ProjectsManagingSystem.Controllers
{
    [Route("api/project/task")]
    [ApiController]
    public class ProjectTaskController: ControllerBase

    {
        private readonly IProjectTaskService _projectTaskService;

        public ProjectTaskController(IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Task>> GetAll()
        {
            var tasks = _projectTaskService.GetAll();

            return Ok(tasks);

        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var task = _projectTaskService.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);

        }

    }
}
