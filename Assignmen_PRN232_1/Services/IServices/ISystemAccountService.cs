using Assignmen_PRN232__.Dto;
using Assignmen_PRN232__.Dto.Common;
using Assignmen_PRN232_1.DTOs.Common;

namespace Assignmen_PRN232_1.Services.IServices
{
    public interface ISystemAccountService
    {
        Task<ApiResponse<PagingResponse<SystemAccountDto>>> GetListPagingAsync(SystemAccountSearchDto dto);
        Task<ApiResponse<IEnumerable<SystemAccountDto>>> GetAllAsync();
        Task<ApiResponse<SystemAccountDto>> GetByIdAsync(short id);

        Task<ApiResponse<SystemAccountDto>> CreateOrEditAsync(SystemAccountSaveDto dto);
        Task<ApiResponse<bool>> DeleteAsync(short id);

        Task<ApiResponse<SystemAccountDto>> LoginAsync(SystemAccountLoginDto dto);
    }
}
