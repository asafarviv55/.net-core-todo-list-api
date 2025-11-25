namespace ToDoList.Models
{
    public class TaskSummaryReport
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int OverdueTasks { get; set; }
        public double CompletionRate { get; set; }
        public DateTime ReportGeneratedAt { get; set; }
    }

    public class CategoryReport
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TaskCount { get; set; }
        public int CompletedCount { get; set; }
        public int PendingCount { get; set; }
    }

    public class UserProductivityReport
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int AssignedTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public double CompletionRate { get; set; }
        public TimeSpan AverageCompletionTime { get; set; }
    }

    public class PriorityDistribution
    {
        public string Priority { get; set; }
        public int Count { get; set; }
        public int CompletedCount { get; set; }
        public int PendingCount { get; set; }
    }
}
