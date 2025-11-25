using ToDoList.Models;

namespace ToDoList.Services
{
    public class AttachmentService
    {
        private static List<TaskAttachment> attachments = new List<TaskAttachment>();
        private static int nextId = 1;

        public static List<TaskAttachment> GetAllAttachments()
        {
            return attachments;
        }

        public static TaskAttachment GetAttachmentById(int id)
        {
            return attachments.FirstOrDefault(a => a.Id == id);
        }

        public static List<TaskAttachment> GetAttachmentsByTask(int taskId)
        {
            return attachments.Where(a => a.TaskId == taskId).OrderByDescending(a => a.UploadedAt).ToList();
        }

        public static TaskAttachment AddAttachment(TaskAttachment attachment)
        {
            attachment.Id = nextId++;
            attachment.UploadedAt = DateTime.Now;
            attachments.Add(attachment);

            TaskHistoryService.LogAction(attachment.TaskId, attachment.UploadedByUserId, "User",
                TaskActions.AttachmentAdded, "", "", "", $"File attached: {attachment.FileName}");

            return attachment;
        }

        public static bool DeleteAttachment(int id)
        {
            var attachment = attachments.FirstOrDefault(a => a.Id == id);
            if (attachment != null)
            {
                attachments.Remove(attachment);
                return true;
            }
            return false;
        }

        public static long GetTotalAttachmentSize(int taskId)
        {
            return attachments.Where(a => a.TaskId == taskId).Sum(a => a.FileSize);
        }

        public static int GetAttachmentCount(int taskId)
        {
            return attachments.Count(a => a.TaskId == taskId);
        }
    }
}
