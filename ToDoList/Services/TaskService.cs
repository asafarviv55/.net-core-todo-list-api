using ToDoList.Models;

namespace ToDoList.Services
{
    public class TaskService
    {
        private static List<TodoTask> tasks = new List<TodoTask>();
        private static int nextId = 1;

        public static List<TodoTask> GetAllTasks()
        {
            return tasks;
        }

        public static TodoTask GetTaskById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public static TodoTask CreateTask(TodoTask task)
        {
            task.Id = nextId++;
            task.CreatedAt = DateTime.Now;
            task.Status = "Open";
            task.IsCompleted = false;
            tasks.Add(task);

            TaskHistoryService.LogAction(task.Id, 0, "System", TaskActions.Created,
                "", "", "", $"Task '{task.Title}' created");

            return task;
        }

        public static bool UpdateTask(int id, TodoTask task)
        {
            var existing = tasks.FirstOrDefault(t => t.Id == id);
            if (existing != null)
            {
                existing.Title = task.Title;
                existing.Description = task.Description;
                existing.DueDate = task.DueDate;
                existing.CategoryId = task.CategoryId;
                existing.Priority = task.Priority;
                existing.Status = task.Status;

                TaskHistoryService.LogAction(id, 0, "System", TaskActions.Updated,
                    "", "", "", $"Task updated");

                return true;
            }
            return false;
        }

        public static bool DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                TaskHistoryService.LogAction(id, 0, "System", TaskActions.Deleted,
                    "", "", "", $"Task deleted");
                return true;
            }
            return false;
        }

        public static bool CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                task.CompletedAt = DateTime.Now;
                task.Status = "Completed";

                TaskHistoryService.LogAction(id, 0, "System", TaskActions.Completed,
                    "IsCompleted", "false", "true", $"Task marked as completed");

                return true;
            }
            return false;
        }

        public static bool ReopenTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = false;
                task.CompletedAt = null;
                task.Status = "Open";

                TaskHistoryService.LogAction(id, 0, "System", TaskActions.Reopened,
                    "IsCompleted", "true", "false", $"Task reopened");

                return true;
            }
            return false;
        }

        public static List<TodoTask> GetTasksByCategory(int categoryId)
        {
            return tasks.Where(t => t.CategoryId == categoryId).ToList();
        }

        public static List<TodoTask> GetTasksByPriority(string priority)
        {
            return tasks.Where(t => t.Priority == priority).ToList();
        }

        public static List<TodoTask> GetTasksByStatus(string status)
        {
            return tasks.Where(t => t.Status == status).ToList();
        }

        public static List<TodoTask> GetOverdueTasks()
        {
            return tasks.Where(t => !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value < DateTime.Now).ToList();
        }

        public static List<TodoTask> SearchTasks(string searchTerm)
        {
            return tasks.Where(t =>
                t.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (t.Description != null && t.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
    }
}
