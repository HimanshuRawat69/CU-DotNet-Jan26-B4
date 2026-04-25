using CourseAPI.Data;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseAPIDbContext _context;

        public CourseRepository(CourseAPIDbContext context) => _context = context;

        public async Task<Course> AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<List<Course>> GetAllAsync() =>
            await _context.Courses.ToListAsync();

        public async Task<Course?> GetByIdAsync(int id) =>
            await _context.Courses.FindAsync(id);

        public async Task<Course> UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return course;
        }
    }



}
