namespace ToDoList.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; }
        public int? CategoryId { get; set; }
        public string Priority { get; set; }
        public int? AssignedToUserId { get; set; }
        public string Status { get; set; }
    }
}
