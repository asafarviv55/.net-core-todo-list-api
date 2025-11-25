using ToDoList.Models;

namespace ToDoList.Services
{
    public class PriorityService
    {
        private static List<TaskPriority> priorities = new List<TaskPriority>
        {
            new TaskPriority { Id = 1, Name = PriorityLevels.Low, Level = 1, Color = "#28a745", Description = "Low priority tasks" },
            new TaskPriority { Id = 2, Name = PriorityLevels.Medium, Level = 2, Color = "#ffc107", Description = "Medium priority tasks" },
            new TaskPriority { Id = 3, Name = PriorityLevels.High, Level = 3, Color = "#fd7e14", Description = "High priority tasks" },
            new TaskPriority { Id = 4, Name = PriorityLevels.Urgent, Level = 4, Color = "#dc3545", Description = "Urgent tasks" },
            new TaskPriority { Id = 5, Name = PriorityLevels.Critical, Level = 5, Color = "#721c24", Description = "Critical priority tasks" }
        };

        public static List<TaskPriority> GetAllPriorities()
        {
            return priorities.OrderBy(p => p.Level).ToList();
        }

        public static TaskPriority GetPriorityByName(string name)
        {
            return priorities.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public static TaskPriority GetPriorityById(int id)
        {
            return priorities.FirstOrDefault(p => p.Id == id);
        }
    }
}
