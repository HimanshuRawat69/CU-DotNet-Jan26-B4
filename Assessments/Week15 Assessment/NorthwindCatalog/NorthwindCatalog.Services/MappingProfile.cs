
using AutoMapper;
using NorthwindCatalog.Services.Models;
using NorthwindCatalog.Services.DTOs;
namespace NorthwindCatalog.Services
{
    

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => "/images/" + src.CategoryName + ".jpg"));

            CreateMap<Product, ProductDto>();
        }
    }

}
