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

        [HttpGet("{id:int}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _taskService.GetById(id);
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

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTask([FromRoute] int id)
        {
            var isDeleted = _taskService.Delete(id);
            if (isDeleted)
            {
                return NoContent(); 
            }
            return NotFound();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateTask([FromBody] ProjectTaskDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = _taskService.Update(dto, id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
