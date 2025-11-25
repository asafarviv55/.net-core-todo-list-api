using ToDoList.Models;

namespace ToDoList.Services
{
    public class ReminderService
    {
        private static List<TaskReminder> reminders = new List<TaskReminder>();
        private static int nextId = 1;

        public static List<TaskReminder> GetAllReminders()
        {
            return reminders;
        }

        public static TaskReminder GetReminderById(int id)
        {
            return reminders.FirstOrDefault(r => r.Id == id);
        }

        public static List<TaskReminder> GetRemindersByTask(int taskId)
        {
            return reminders.Where(r => r.TaskId == taskId).OrderBy(r => r.ReminderTime).ToList();
        }

        public static TaskReminder CreateReminder(TaskReminder reminder)
        {
            reminder.Id = nextId++;
            reminder.IsSent = false;
            reminders.Add(reminder);
            return reminder;
        }

        public static bool UpdateReminder(int id, TaskReminder reminder)
        {
            var existing = reminders.FirstOrDefault(r => r.Id == id);
            if (existing != null)
            {
                existing.ReminderTime = reminder.ReminderTime;
                existing.ReminderType = reminder.ReminderType;
                existing.Message = reminder.Message;
                existing.IsRecurring = reminder.IsRecurring;
                existing.RecurringPattern = reminder.RecurringPattern;
                return true;
            }
            return false;
        }

        public static bool DeleteReminder(int id)
        {
            var reminder = reminders.FirstOrDefault(r => r.Id == id);
            if (reminder != null)
            {
                reminders.Remove(reminder);
                return true;
            }
            return false;
        }

        public static List<TaskReminder> GetPendingReminders()
        {
            return reminders.Where(r => !r.IsSent && r.ReminderTime <= DateTime.Now).ToList();
        }

        public static bool MarkReminderAsSent(int id)
        {
            var reminder = reminders.FirstOrDefault(r => r.Id == id);
            if (reminder != null)
            {
                reminder.IsSent = true;
                reminder.SentAt = DateTime.Now;
                return true;
            }
            return false;
        }

        public static List<TaskReminder> GetUserReminders(int userId)
        {
            return reminders.Where(r => r.UserId == userId && !r.IsSent).OrderBy(r => r.ReminderTime).ToList();
        }
    }
}
