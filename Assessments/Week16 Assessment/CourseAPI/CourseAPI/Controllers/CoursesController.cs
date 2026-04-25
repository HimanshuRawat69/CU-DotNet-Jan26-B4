using CourseAPI.Common;
using CourseAPI.DTOs;
using CourseAPI.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(new ApiResponse<CourseResponseDto> { Success = true, Message = "Course Created", Data = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<CourseResponseDto>> { Success = true, Data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(new ApiResponse<CourseResponseDto> { Success = true, Data = result });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCourseDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(new ApiResponse<CourseResponseDto> { Success = true, Message = "Course Updated", Data = result });
        }
    }

}
