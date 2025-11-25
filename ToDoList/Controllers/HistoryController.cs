using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskHistory>> GetAll()
        {
            return Ok(TaskHistoryService.GetAllHistory());
        }

        [HttpGet("task/{taskId}")]
        public ActionResult<List<TaskHistory>> GetTaskHistory(int taskId)
        {
            return Ok(TaskHistoryService.GetTaskHistory(taskId));
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<TaskHistory>> GetUserHistory(int userId)
        {
            return Ok(TaskHistoryService.GetUserHistory(userId));
        }

        [HttpGet("daterange")]
        public ActionResult<List<TaskHistory>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(TaskHistoryService.GetHistoryByDateRange(startDate, endDate));
        }

        [HttpGet("action/{action}")]
        public ActionResult<List<TaskHistory>> GetByAction(string action)
        {
            return Ok(TaskHistoryService.GetHistoryByAction(action));
        }
    }
}
