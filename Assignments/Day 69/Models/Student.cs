namespace Day69.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }

        // Navigation property for Many-to-Many
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}