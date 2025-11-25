using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TodoTask>> GetAll()
        {
            return Ok(TaskService.GetAllTasks());
        }

        [HttpGet("{id}")]
        public ActionResult<TodoTask> GetById(int id)
        {
            var task = TaskService.GetTaskById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TodoTask> Create([FromBody] TodoTask task)
        {
            var created = TaskService.CreateTask(task);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] TodoTask task)
        {
            if (TaskService.UpdateTask(id, task))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (TaskService.DeleteTask(id))
                return NoContent();
            return NotFound();
        }

        [HttpPut("{id}/complete")]
        public ActionResult Complete(int id)
        {
            if (TaskService.CompleteTask(id))
                return NoContent();
            return NotFound();
        }

        [HttpPut("{id}/reopen")]
        public ActionResult Reopen(int id)
        {
            if (TaskService.ReopenTask(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult<List<TodoTask>> GetByCategory(int categoryId)
        {
            return Ok(TaskService.GetTasksByCategory(categoryId));
        }

        [HttpGet("priority/{priority}")]
        public ActionResult<List<TodoTask>> GetByPriority(string priority)
        {
            return Ok(TaskService.GetTasksByPriority(priority));
        }

        [HttpGet("status/{status}")]
        public ActionResult<List<TodoTask>> GetByStatus(string status)
        {
            return Ok(TaskService.GetTasksByStatus(status));
        }

        [HttpGet("overdue")]
        public ActionResult<List<TodoTask>> GetOverdue()
        {
            return Ok(TaskService.GetOverdueTasks());
        }

        [HttpGet("search")]
        public ActionResult<List<TodoTask>> Search([FromQuery] string term)
        {
            return Ok(TaskService.SearchTasks(term));
        }
    }
}
