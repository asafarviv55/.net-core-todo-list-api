using ToDoList.Models;

namespace ToDoList.Services
{
    public class CategoryService
    {
        private static List<TaskCategory> categories = new List<TaskCategory>();
        private static int nextId = 1;

        public static List<TaskCategory> GetAllCategories()
        {
            return categories;
        }

        public static TaskCategory GetCategoryById(int id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }

        public static TaskCategory CreateCategory(TaskCategory category)
        {
            category.Id = nextId++;
            category.CreatedAt = DateTime.Now;
            categories.Add(category);
            return category;
        }

        public static bool UpdateCategory(int id, TaskCategory category)
        {
            var existing = categories.FirstOrDefault(c => c.Id == id);
            if (existing != null)
            {
                existing.Name = category.Name;
                existing.Description = category.Description;
                existing.Color = category.Color;
                existing.Icon = category.Icon;
                existing.DisplayOrder = category.DisplayOrder;
                existing.IsActive = category.IsActive;
                return true;
            }
            return false;
        }

        public static bool DeleteCategory(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                categories.Remove(category);
                return true;
            }
            return false;
        }

        public static List<TaskCategory> GetActiveCategories()
        {
            return categories.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder).ToList();
        }
    }
}
