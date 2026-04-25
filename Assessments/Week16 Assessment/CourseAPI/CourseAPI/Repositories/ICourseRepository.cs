using CourseAPI.Models;

namespace CourseAPI.Repositories
{
    public interface ICourseRepository
    {
        Task<Course> AddAsync(Course course);
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course> UpdateAsync(Course course);
    }
}
