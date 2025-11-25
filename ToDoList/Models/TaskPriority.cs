namespace ToDoList.Models
{
    public class TaskPriority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
    }

    public static class PriorityLevels
    {
        public const string Low = "Low";
        public const string Medium = "Medium";
        public const string High = "High";
        public const string Urgent = "Urgent";
        public const string Critical = "Critical";
    }
}
