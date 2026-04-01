using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Day69.Data;
using Day69.DTOs;

namespace Day69.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> EnrollStudent([FromBody] EnrollmentDto enrollmentDto)
        {
            var student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == enrollmentDto.StudentId);

            var course = await _context.Courses.FindAsync(enrollmentDto.CourseId);

            if (student == null || course == null)
            {
                return NotFound("Student or Course not found.");
            }

            if (student.Courses.Any(c => c.Id == course.Id))
            {
                return BadRequest("Student is already enrolled in this course.");
            }

            student.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Student successfully enrolled in the course." });
        }
    }
}