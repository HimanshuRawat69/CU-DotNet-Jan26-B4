using CourseAPI.DTOs;

namespace CourseAPI.Services
{
    public interface ICourseService
    {
        Task<CourseResponseDto> CreateAsync(CreateCourseDto dto);
        Task<List<CourseResponseDto>> GetAllAsync();
        Task<CourseResponseDto>? GetByIdAsync(int id);
        Task<CourseResponseDto> UpdateAsync(UpdateCourseDto dto);
    }
}
