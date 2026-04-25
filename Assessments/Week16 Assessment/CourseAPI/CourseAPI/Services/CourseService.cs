using AutoMapper;
using CourseAPI.DTOs;
using CourseAPI.Models;
using CourseAPI.Repositories;

namespace CourseAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        private double CalculateDiscountedPrice(double price)
        {

            return Math.Round(price * 0.90, 2);
        }
        public async Task<CourseResponseDto> CreateAsync(CreateCourseDto dto)
        {
            if (dto.Price > 1000)
                throw new Exception("Course price exceeds maximum allowed limit.");

            var course = _mapper.Map<Course>(dto);
            course.DiscountedPrice = CalculateDiscountedPrice(dto.Price);
            course.CreatedDate = DateTime.UtcNow;

            var result = await _repo.AddAsync(course);
            return _mapper.Map<CourseResponseDto>(result);
        }

        public async Task<List<CourseResponseDto>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();
            return _mapper.Map<List<CourseResponseDto>>(data);
        }

        public async Task<CourseResponseDto?> GetByIdAsync(int id)
        {
            var course = await _repo.GetByIdAsync(id);
            if (course == null) throw new Exception("Course not found.");
            return _mapper.Map<CourseResponseDto>(course);
        }

        public async Task<CourseResponseDto> UpdateAsync(UpdateCourseDto dto)
        {
            if (dto.Price > 1000)
                throw new Exception("Course price exceeds maximum allowed limit.");

            var course = await _repo.GetByIdAsync(dto.CourseId);
            if (course == null) throw new Exception("Course not found.");

            _mapper.Map(dto, course);
            course.DiscountedPrice = CalculateDiscountedPrice(course.Price);

            var result = await _repo.UpdateAsync(course);
            return _mapper.Map<CourseResponseDto>(result);
        }


    }
}
