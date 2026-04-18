using NorthwindCatalog.Services.Models;

namespace NorthwindCatalog.Services.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
