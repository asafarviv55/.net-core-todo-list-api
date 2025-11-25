namespace ToDoList.Models
{
    public class TaskReminder
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime ReminderTime { get; set; }
        public string ReminderType { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentAt { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public bool IsRecurring { get; set; }
        public string RecurringPattern { get; set; }
    }

    public static class ReminderTypes
    {
        public const string Email = "Email";
        public const string SMS = "SMS";
        public const string Push = "Push";
        public const string InApp = "InApp";
    }
}
