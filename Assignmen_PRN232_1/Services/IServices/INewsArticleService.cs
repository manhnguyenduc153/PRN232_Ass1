using Assignmen_PRN232__.Dto;
using Assignmen_PRN232__.Dto.Common;
using Assignmen_PRN232_1.DTOs.Common;

namespace Assignmen_PRN232_1.Services.IServices
{
    public interface INewsArticleService
    {
        Task<ApiResponse<PagingResponse<NewsArticleDto>>> GetListPagingAsync(NewsArticleSearchDto dto);
        Task<ApiResponse<IEnumerable<NewsArticleDto>>> GetAllAsync();
        Task<ApiResponse<NewsArticleDto>> GetByIdAsync(string id);

        Task<ApiResponse<NewsArticleDto>> CreateOrEditAsync(NewsArticleSaveDto dto);
        Task<ApiResponse<bool>> DeleteAsync(string id);
    }
}
