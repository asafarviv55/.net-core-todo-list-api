using ToDoList.Models;

namespace ToDoList.Services
{
    public class TagService
    {
        private static List<TaskTag> tags = new List<TaskTag>();
        private static List<TaskTagMapping> tagMappings = new List<TaskTagMapping>();
        private static int nextTagId = 1;
        private static int nextMappingId = 1;

        public static List<TaskTag> GetAllTags()
        {
            return tags;
        }

        public static TaskTag GetTagById(int id)
        {
            return tags.FirstOrDefault(t => t.Id == id);
        }

        public static TaskTag CreateTag(TaskTag tag)
        {
            tag.Id = nextTagId++;
            tag.CreatedAt = DateTime.Now;
            tags.Add(tag);
            return tag;
        }

        public static bool UpdateTag(int id, TaskTag tag)
        {
            var existing = tags.FirstOrDefault(t => t.Id == id);
            if (existing != null)
            {
                existing.Name = tag.Name;
                existing.Color = tag.Color;
                return true;
            }
            return false;
        }

        public static bool DeleteTag(int id)
        {
            var tag = tags.FirstOrDefault(t => t.Id == id);
            if (tag != null)
            {
                tags.Remove(tag);
                tagMappings.RemoveAll(tm => tm.TagId == id);
                return true;
            }
            return false;
        }

        public static bool AssignTagToTask(int taskId, int tagId)
        {
            if (!tagMappings.Any(tm => tm.TaskId == taskId && tm.TagId == tagId))
            {
                var mapping = new TaskTagMapping
                {
                    Id = nextMappingId++,
                    TaskId = taskId,
                    TagId = tagId,
                    AssignedAt = DateTime.Now
                };
                tagMappings.Add(mapping);
                return true;
            }
            return false;
        }

        public static bool RemoveTagFromTask(int taskId, int tagId)
        {
            var mapping = tagMappings.FirstOrDefault(tm => tm.TaskId == taskId && tm.TagId == tagId);
            if (mapping != null)
            {
                tagMappings.Remove(mapping);
                return true;
            }
            return false;
        }

        public static List<TaskTag> GetTaskTags(int taskId)
        {
            var tagIds = tagMappings.Where(tm => tm.TaskId == taskId).Select(tm => tm.TagId).ToList();
            return tags.Where(t => tagIds.Contains(t.Id)).ToList();
        }

        public static List<TodoTask> GetTasksByTag(int tagId)
        {
            var taskIds = tagMappings.Where(tm => tm.TagId == tagId).Select(tm => tm.TaskId).ToList();
            return TaskService.GetAllTasks().Where(t => taskIds.Contains(t.Id)).ToList();
        }
    }
}
