using ToDoList.Models;

namespace ToDoList.Services
{
    public class TaskHistoryService
    {
        private static List<TaskHistory> history = new List<TaskHistory>();
        private static int nextId = 1;

        public static List<TaskHistory> GetAllHistory()
        {
            return history;
        }

        public static List<TaskHistory> GetTaskHistory(int taskId)
        {
            return history.Where(h => h.TaskId == taskId).OrderByDescending(h => h.ChangedAt).ToList();
        }

        public static List<TaskHistory> GetUserHistory(int userId)
        {
            return history.Where(h => h.UserId == userId).OrderByDescending(h => h.ChangedAt).ToList();
        }

        public static void LogAction(int taskId, int userId, string userName, string action,
            string fieldChanged, string oldValue, string newValue, string description)
        {
            var historyEntry = new TaskHistory
            {
                Id = nextId++,
                TaskId = taskId,
                UserId = userId,
                UserName = userName,
                Action = action,
                FieldChanged = fieldChanged,
                OldValue = oldValue,
                NewValue = newValue,
                ChangedAt = DateTime.Now,
                Description = description
            };
            history.Add(historyEntry);
        }

        public static List<TaskHistory> GetHistoryByDateRange(DateTime startDate, DateTime endDate)
        {
            return history.Where(h => h.ChangedAt >= startDate && h.ChangedAt <= endDate)
                         .OrderByDescending(h => h.ChangedAt).ToList();
        }

        public static List<TaskHistory> GetHistoryByAction(string action)
        {
            return history.Where(h => h.Action == action).OrderByDescending(h => h.ChangedAt).ToList();
        }
    }
}
