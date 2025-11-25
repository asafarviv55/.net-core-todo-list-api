namespace ToDoList.Models
{
    public class TaskHistory
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string FieldChanged { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangedAt { get; set; }
        public string Description { get; set; }
    }

    public static class TaskActions
    {
        public const string Created = "Created";
        public const string Updated = "Updated";
        public const string Deleted = "Deleted";
        public const string Completed = "Completed";
        public const string Reopened = "Reopened";
        public const string Assigned = "Assigned";
        public const string Unassigned = "Unassigned";
        public const string CommentAdded = "CommentAdded";
        public const string AttachmentAdded = "AttachmentAdded";
    }
}
