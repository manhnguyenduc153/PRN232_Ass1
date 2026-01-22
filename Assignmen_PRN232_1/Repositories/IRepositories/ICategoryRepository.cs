using Assignmen_PRN232__.Dto;
using Assignmen_PRN232__.Models;
using Assignmen_PRN232_1.DTOs.Common;

namespace Assignmen_PRN232__.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<PagingResponse<CategoryDto>> GetListPagingAsync(CategorySearchDto searchDto);
        Task<Category?> GetByIdAsync<TKey>(TKey id) where TKey : notnull;
        Task<Category?> GetByIdAsync(short id);
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> ExistsByNameAsync(string categoryName);
        Task<int> SaveChangesAsync();
    }
}
