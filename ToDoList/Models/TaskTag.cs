namespace ToDoList.Models
{
    public class TaskTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TaskTagMapping
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int TagId { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
