namespace Day69.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }

        // Navigation property for Many-to-Many
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}