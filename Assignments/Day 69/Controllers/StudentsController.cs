using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Day69.Data;
using Day69.DTOs;
using Day69.Models;

namespace Day69.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _context.Students
                .Select(s => new StudentDto { Id = s.Id, Name = s.Name, Email = s.Email, Age = s.Age })
                .ToListAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            return Ok(new StudentDto { Id = student.Id, Name = student.Name, Email = student.Email, Age = student.Age });
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> PostStudent(StudentCreateDto studentDto)
        {
            // Manual validation for unique email since DB throws an exception otherwise
            if (await _context.Students.AnyAsync(s => s.Email == studentDto.Email))
            {
                return BadRequest("Email already exists.");
            }

            var student = new Student
            {
                Name = studentDto.Name,
                Email = studentDto.Email,
                Age = studentDto.Age
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var createdDto = new StudentDto { Id = student.Id, Name = student.Name, Email = student.Email, Age = student.Age };
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentCreateDto studentDto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            if (student.Email != studentDto.Email && await _context.Students.AnyAsync(s => s.Email == studentDto.Email))
            {
                return BadRequest("Email already exists.");
            }

            student.Name = studentDto.Name;
            student.Email = studentDto.Email;
            student.Age = studentDto.Age;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}