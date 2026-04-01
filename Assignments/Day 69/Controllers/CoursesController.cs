using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Day69.Data;
using Day69.DTOs;
using Day69.Models;

namespace Day69.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _context.Courses
                .Select(c => new CourseDto { Id = c.Id, Title = c.Title, Credits = c.Credits })
                .ToListAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            return Ok(new CourseDto { Id = course.Id, Title = course.Title, Credits = course.Credits });
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> PostCourse(CourseCreateDto courseDto)
        {
            var course = new Course
            {
                Title = courseDto.Title,
                Credits = courseDto.Credits
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var createdDto = new CourseDto { Id = course.Id, Title = course.Title, Credits = course.Credits };
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseCreateDto courseDto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            course.Title = courseDto.Title;
            course.Credits = courseDto.Credits;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}