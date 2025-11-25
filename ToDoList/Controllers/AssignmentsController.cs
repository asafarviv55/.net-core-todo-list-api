using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskAssignment>> GetAll()
        {
            return Ok(AssignmentService.GetAllAssignments());
        }

        [HttpGet("task/{taskId}")]
        public ActionResult<List<TaskAssignment>> GetTaskAssignments(int taskId)
        {
            return Ok(AssignmentService.GetTaskAssignments(taskId));
        }

        [HttpGet("user/{userId}/tasks")]
        public ActionResult<List<TodoTask>> GetUserTasks(int userId)
        {
            return Ok(AssignmentService.GetUserAssignedTasks(userId));
        }

        [HttpPost]
        public ActionResult<TaskAssignment> Assign([FromBody] AssignmentRequest request)
        {
            var assignment = AssignmentService.AssignTask(request.TaskId, request.UserId, request.AssignedByUserId);
            if (assignment == null) return BadRequest();
            return Ok(assignment);
        }

        [HttpDelete("task/{taskId}/user/{userId}")]
        public ActionResult Unassign(int taskId, int userId)
        {
            if (AssignmentService.UnassignTask(taskId, userId))
                return NoContent();
            return NotFound();
        }

        [HttpGet("users")]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(AssignmentService.GetAllUsers());
        }

        [HttpGet("users/{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = AssignmentService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("users")]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            var created = AssignmentService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = created.Id }, created);
        }
    }

    public class AssignmentRequest
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int AssignedByUserId { get; set; }
    }
}
