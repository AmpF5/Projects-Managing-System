using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Models.ProjectTask;
using ProjectsManagingSystem.Services.Task;

namespace ProjectsManagingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{taskId:int}")]
        public IActionResult GetTaskById(int taskId)
        {
            var task = _taskService.GetById(taskId);
            return Ok(task);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProjectTaskResponseDto>> GetAllTasks()
        {
            var tasks = _taskService.GetAll();
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] ProjectTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _taskService.Create(dto);
            return Created($"{id}", null);
        }

        [HttpDelete("{taskId:int}")]
        public IActionResult DeleteTask([FromRoute] int taskId)
        {
            var isDeleted = _taskService.Delete(taskId);
            if (isDeleted)
            {
                return NoContent(); 
            }
            return NotFound();
        }

        [HttpPut("{taskId:int}")]
        public IActionResult UpdateTask([FromBody] ProjectTaskDto dto, [FromRoute] int taskId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = _taskService.Update(dto, taskId);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
