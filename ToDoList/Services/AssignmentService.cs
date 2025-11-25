using ToDoList.Models;

namespace ToDoList.Services
{
    public class AssignmentService
    {
        private static List<TaskAssignment> assignments = new List<TaskAssignment>();
        private static List<User> users = new List<User>();
        private static int nextAssignmentId = 1;
        private static int nextUserId = 1;

        public static List<TaskAssignment> GetAllAssignments()
        {
            return assignments;
        }

        public static List<User> GetAllUsers()
        {
            return users;
        }

        public static User GetUserById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public static User CreateUser(User user)
        {
            user.Id = nextUserId++;
            user.CreatedAt = DateTime.Now;
            users.Add(user);
            return user;
        }

        public static TaskAssignment AssignTask(int taskId, int userId, int assignedByUserId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                var assignment = new TaskAssignment
                {
                    Id = nextAssignmentId++,
                    TaskId = taskId,
                    UserId = userId,
                    UserName = user.Name,
                    UserEmail = user.Email,
                    AssignedAt = DateTime.Now,
                    AssignedByUserId = assignedByUserId,
                    Role = user.Role,
                    IsActive = true
                };
                assignments.Add(assignment);

                TaskHistoryService.LogAction(taskId, assignedByUserId, "Assigner",
                    TaskActions.Assigned, "", "", "", $"Task assigned to {user.Name}");

                return assignment;
            }
            return null;
        }

        public static bool UnassignTask(int taskId, int userId)
        {
            var assignment = assignments.FirstOrDefault(a => a.TaskId == taskId && a.UserId == userId && a.IsActive);
            if (assignment != null)
            {
                assignment.IsActive = false;

                TaskHistoryService.LogAction(taskId, userId, "System",
                    TaskActions.Unassigned, "", "", "", $"Task unassigned from {assignment.UserName}");

                return true;
            }
            return false;
        }

        public static List<TaskAssignment> GetTaskAssignments(int taskId)
        {
            return assignments.Where(a => a.TaskId == taskId && a.IsActive).ToList();
        }

        public static List<TodoTask> GetUserAssignedTasks(int userId)
        {
            var taskIds = assignments.Where(a => a.UserId == userId && a.IsActive).Select(a => a.TaskId).ToList();
            return TaskService.GetAllTasks().Where(t => taskIds.Contains(t.Id)).ToList();
        }
    }
}
