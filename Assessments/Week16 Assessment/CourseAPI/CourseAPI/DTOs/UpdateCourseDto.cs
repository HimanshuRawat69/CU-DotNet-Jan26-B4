namespace CourseAPI.DTOs
{
    public class UpdateCourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Summary { get; set; }

    }
}
