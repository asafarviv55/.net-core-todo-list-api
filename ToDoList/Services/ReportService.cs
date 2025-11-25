using ToDoList.Models;

namespace ToDoList.Services
{
    public class ReportService
    {
        public static TaskSummaryReport GenerateSummaryReport()
        {
            var allTasks = TaskService.GetAllTasks();
            var completedTasks = allTasks.Where(t => t.IsCompleted).ToList();
            var pendingTasks = allTasks.Where(t => !t.IsCompleted).ToList();
            var overdueTasks = TaskService.GetOverdueTasks();

            return new TaskSummaryReport
            {
                TotalTasks = allTasks.Count,
                CompletedTasks = completedTasks.Count,
                PendingTasks = pendingTasks.Count,
                OverdueTasks = overdueTasks.Count,
                CompletionRate = allTasks.Count > 0 ? (double)completedTasks.Count / allTasks.Count * 100 : 0,
                ReportGeneratedAt = DateTime.Now
            };
        }

        public static List<CategoryReport> GenerateCategoryReport()
        {
            var categories = CategoryService.GetAllCategories();
            var reports = new List<CategoryReport>();

            foreach (var category in categories)
            {
                var categoryTasks = TaskService.GetTasksByCategory(category.Id);
                reports.Add(new CategoryReport
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    TaskCount = categoryTasks.Count,
                    CompletedCount = categoryTasks.Count(t => t.IsCompleted),
                    PendingCount = categoryTasks.Count(t => !t.IsCompleted)
                });
            }

            return reports.OrderByDescending(r => r.TaskCount).ToList();
        }

        public static List<UserProductivityReport> GenerateUserProductivityReport()
        {
            var users = AssignmentService.GetAllUsers();
            var reports = new List<UserProductivityReport>();

            foreach (var user in users)
            {
                var userTasks = AssignmentService.GetUserAssignedTasks(user.Id);
                var completedTasks = userTasks.Where(t => t.IsCompleted).ToList();

                var avgCompletionTime = TimeSpan.Zero;
                if (completedTasks.Any())
                {
                    var totalTime = completedTasks
                        .Where(t => t.CompletedAt.HasValue)
                        .Select(t => (t.CompletedAt.Value - t.CreatedAt).TotalMinutes)
                        .Average();
                    avgCompletionTime = TimeSpan.FromMinutes(totalTime);
                }

                reports.Add(new UserProductivityReport
                {
                    UserId = user.Id,
                    UserName = user.Name,
                    AssignedTasks = userTasks.Count,
                    CompletedTasks = completedTasks.Count,
                    PendingTasks = userTasks.Count - completedTasks.Count,
                    CompletionRate = userTasks.Count > 0 ? (double)completedTasks.Count / userTasks.Count * 100 : 0,
                    AverageCompletionTime = avgCompletionTime
                });
            }

            return reports.OrderByDescending(r => r.CompletionRate).ToList();
        }

        public static List<PriorityDistribution> GeneratePriorityDistribution()
        {
            var priorities = PriorityService.GetAllPriorities();
            var distribution = new List<PriorityDistribution>();

            foreach (var priority in priorities)
            {
                var priorityTasks = TaskService.GetTasksByPriority(priority.Name);
                distribution.Add(new PriorityDistribution
                {
                    Priority = priority.Name,
                    Count = priorityTasks.Count,
                    CompletedCount = priorityTasks.Count(t => t.IsCompleted),
                    PendingCount = priorityTasks.Count(t => !t.IsCompleted)
                });
            }

            return distribution;
        }

        public static Dictionary<string, int> GetTaskStatusDistribution()
        {
            var allTasks = TaskService.GetAllTasks();
            return allTasks.GroupBy(t => t.Status)
                          .ToDictionary(g => g.Key, g => g.Count());
        }

        public static Dictionary<DateTime, int> GetTaskCreationTrend(int days)
        {
            var allTasks = TaskService.GetAllTasks();
            var startDate = DateTime.Now.AddDays(-days);

            return allTasks
                .Where(t => t.CreatedAt >= startDate)
                .GroupBy(t => t.CreatedAt.Date)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
