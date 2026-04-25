namespace CourseAPI.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Summary { get; set; }
        public double DiscountedPrice { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
