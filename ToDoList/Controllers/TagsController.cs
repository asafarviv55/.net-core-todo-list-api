using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskTag>> GetAll()
        {
            return Ok(TagService.GetAllTags());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskTag> GetById(int id)
        {
            var tag = TagService.GetTagById(id);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpPost]
        public ActionResult<TaskTag> Create([FromBody] TaskTag tag)
        {
            var created = TagService.CreateTag(tag);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] TaskTag tag)
        {
            if (TagService.UpdateTag(id, tag))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (TagService.DeleteTag(id))
                return NoContent();
            return NotFound();
        }

        [HttpPost("task/{taskId}/tag/{tagId}")]
        public ActionResult AssignToTask(int taskId, int tagId)
        {
            if (TagService.AssignTagToTask(taskId, tagId))
                return NoContent();
            return BadRequest();
        }

        [HttpDelete("task/{taskId}/tag/{tagId}")]
        public ActionResult RemoveFromTask(int taskId, int tagId)
        {
            if (TagService.RemoveTagFromTask(taskId, tagId))
                return NoContent();
            return NotFound();
        }

        [HttpGet("task/{taskId}")]
        public ActionResult<List<TaskTag>> GetTaskTags(int taskId)
        {
            return Ok(TagService.GetTaskTags(taskId));
        }

        [HttpGet("{tagId}/tasks")]
        public ActionResult<List<TodoTask>> GetTasksByTag(int tagId)
        {
            return Ok(TagService.GetTasksByTag(tagId));
        }
    }
}
