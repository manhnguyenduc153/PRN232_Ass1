using Assignmen_PRN232__.Dto;
using Assignmen_PRN232__.Models;
using Assignmen_PRN232__.Repositories.IRepositories;
using Assignmen_PRN232_1.Data;
using Assignmen_PRN232_1.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace Assignmen_PRN232__.Repositories
{
    public class CategoryRepository
    : BaseRepository<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {
        }

        public async Task<PagingResponse<Category>> GetListPagingAsync(CategorySearchDto searchDto)
        {
            var query = FindAll(); // AsNoTracking

            // 🔍 Search theo Keyword
            if (!string.IsNullOrWhiteSpace(searchDto.Keyword))
            {
                var keyword = searchDto.Keyword.Trim();
                query = query.Where(x =>
                    x.CategoryName.Contains(keyword) ||
                    (x.CategoryDesciption != null && x.CategoryDesciption.Contains(keyword)));
            }

            if (searchDto.Status.HasValue)
            {
                query = query.Where(x => x.IsActive == searchDto.Status.Value);
            }

            var totalRecords = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CategoryId)
                .Skip((searchDto.PageIndex - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .ToListAsync();

            return new PagingResponse<Category>
            {
                PageIndex = searchDto.PageIndex,
                PageSize = searchDto.PageSize,
                TotalRecords = totalRecords,
                Items = items
            };
        }

        public async Task<bool> ExistsByNameAsync(string categoryName)
        {
            return await ExistsAsync(c => c.CategoryName == categoryName);
        }

        // Explicit implementation cho short GetById
        public Task<Category?> GetByIdAsync(short id) => GetByIdAsync<short>(id);
    }
}
