using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskAttachment>> GetAll()
        {
            return Ok(AttachmentService.GetAllAttachments());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskAttachment> GetById(int id)
        {
            var attachment = AttachmentService.GetAttachmentById(id);
            if (attachment == null) return NotFound();
            return Ok(attachment);
        }

        [HttpGet("task/{taskId}")]
        public ActionResult<List<TaskAttachment>> GetByTask(int taskId)
        {
            return Ok(AttachmentService.GetAttachmentsByTask(taskId));
        }

        [HttpPost]
        public ActionResult<TaskAttachment> Create([FromBody] TaskAttachment attachment)
        {
            var created = AttachmentService.AddAttachment(attachment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (AttachmentService.DeleteAttachment(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("task/{taskId}/size")]
        public ActionResult<long> GetTotalSize(int taskId)
        {
            return Ok(AttachmentService.GetTotalAttachmentSize(taskId));
        }

        [HttpGet("task/{taskId}/count")]
        public ActionResult<int> GetCount(int taskId)
        {
            return Ok(AttachmentService.GetAttachmentCount(taskId));
        }
    }
}
