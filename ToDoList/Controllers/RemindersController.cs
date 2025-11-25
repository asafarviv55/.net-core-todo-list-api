using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskReminder>> GetAll()
        {
            return Ok(ReminderService.GetAllReminders());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskReminder> GetById(int id)
        {
            var reminder = ReminderService.GetReminderById(id);
            if (reminder == null) return NotFound();
            return Ok(reminder);
        }

        [HttpGet("task/{taskId}")]
        public ActionResult<List<TaskReminder>> GetByTask(int taskId)
        {
            return Ok(ReminderService.GetRemindersByTask(taskId));
        }

        [HttpPost]
        public ActionResult<TaskReminder> Create([FromBody] TaskReminder reminder)
        {
            var created = ReminderService.CreateReminder(reminder);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] TaskReminder reminder)
        {
            if (ReminderService.UpdateReminder(id, reminder))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (ReminderService.DeleteReminder(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("pending")]
        public ActionResult<List<TaskReminder>> GetPending()
        {
            return Ok(ReminderService.GetPendingReminders());
        }

        [HttpPut("{id}/sent")]
        public ActionResult MarkAsSent(int id)
        {
            if (ReminderService.MarkReminderAsSent(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<TaskReminder>> GetUserReminders(int userId)
        {
            return Ok(ReminderService.GetUserReminders(userId));
        }
    }
}
