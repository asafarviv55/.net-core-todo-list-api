using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskPriority>> GetAll()
        {
            return Ok(PriorityService.GetAllPriorities());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskPriority> GetById(int id)
        {
            var priority = PriorityService.GetPriorityById(id);
            if (priority == null) return NotFound();
            return Ok(priority);
        }

        [HttpGet("name/{name}")]
        public ActionResult<TaskPriority> GetByName(string name)
        {
            var priority = PriorityService.GetPriorityByName(name);
            if (priority == null) return NotFound();
            return Ok(priority);
        }
    }
}
