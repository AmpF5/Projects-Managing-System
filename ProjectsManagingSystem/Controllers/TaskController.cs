using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;
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


        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _taskService.GetById(id);

            return Ok(task);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProjectTaskResponseDto>> GetAll()
        {
            var tasks = _taskService.GetAll();
            return Ok(tasks);
        }

        [HttpPost]
        public ActionResult CreateEducationalMaterial([FromBody] ProjectTaskDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _taskService.Create(dto);


            return Created($"/api/material/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _taskService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }


        [HttpPut("{id}")]
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
