using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;
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

        [HttpGet("Project/{id:int}/tasks")]
        public ActionResult<IEnumerable<ProjectTaskResponseDto>> GetAll(int id)
        {
            var tasks = _taskService.GetAll(id);
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult CreateEducationalMaterial([FromBody] ProjectTaskDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _taskService.Create(dto);


            return Created($"/api/material/{id}", null);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _taskService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }


        [HttpPut("{id:int}")]
        public ActionResult Update([FromBody] ProjectTaskDto dto, [FromRoute] int id)
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
