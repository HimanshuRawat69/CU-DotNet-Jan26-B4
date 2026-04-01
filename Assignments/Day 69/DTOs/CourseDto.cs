namespace Day69.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
    }

    public class CourseCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
    }
}