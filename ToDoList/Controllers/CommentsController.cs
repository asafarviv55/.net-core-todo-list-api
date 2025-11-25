using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskComment>> GetAll()
        {
            return Ok(CommentService.GetAllComments());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskComment> GetById(int id)
        {
            var comment = CommentService.GetCommentById(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpGet("task/{taskId}")]
        public ActionResult<List<TaskComment>> GetByTask(int taskId)
        {
            return Ok(CommentService.GetCommentsByTask(taskId));
        }

        [HttpPost]
        public ActionResult<TaskComment> Create([FromBody] TaskComment comment)
        {
            var created = CommentService.AddComment(comment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] CommentUpdateRequest request)
        {
            if (CommentService.UpdateComment(id, request.Comment))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (CommentService.DeleteComment(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("task/{taskId}/count")]
        public ActionResult<int> GetCount(int taskId)
        {
            return Ok(CommentService.GetCommentCount(taskId));
        }
    }

    public class CommentUpdateRequest
    {
        public string Comment { get; set; }
    }
}
