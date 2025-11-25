using ToDoList.Models;

namespace ToDoList.Services
{
    public class CommentService
    {
        private static List<TaskComment> comments = new List<TaskComment>();
        private static int nextId = 1;

        public static List<TaskComment> GetAllComments()
        {
            return comments;
        }

        public static TaskComment GetCommentById(int id)
        {
            return comments.FirstOrDefault(c => c.Id == id);
        }

        public static List<TaskComment> GetCommentsByTask(int taskId)
        {
            return comments.Where(c => c.TaskId == taskId).OrderByDescending(c => c.CreatedAt).ToList();
        }

        public static TaskComment AddComment(TaskComment comment)
        {
            comment.Id = nextId++;
            comment.CreatedAt = DateTime.Now;
            comment.IsEdited = false;
            comments.Add(comment);

            TaskHistoryService.LogAction(comment.TaskId, comment.UserId, comment.UserName,
                TaskActions.CommentAdded, "", "", "", $"Comment added: {comment.Comment}");

            return comment;
        }

        public static bool UpdateComment(int id, string newComment)
        {
            var existing = comments.FirstOrDefault(c => c.Id == id);
            if (existing != null)
            {
                existing.Comment = newComment;
                existing.UpdatedAt = DateTime.Now;
                existing.IsEdited = true;
                return true;
            }
            return false;
        }

        public static bool DeleteComment(int id)
        {
            var comment = comments.FirstOrDefault(c => c.Id == id);
            if (comment != null)
            {
                comments.Remove(comment);
                return true;
            }
            return false;
        }

        public static int GetCommentCount(int taskId)
        {
            return comments.Count(c => c.TaskId == taskId);
        }
    }
}
