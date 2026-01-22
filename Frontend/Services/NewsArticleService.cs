using Assignmen_PRN232__.Dto;
using Assignmen_PRN232__.Dto.Common;
using Assignmen_PRN232_1.DTOs.Common;
using Frontend.Services.IServices;

namespace Frontend.Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly HttpClient _httpClient;

        public NewsArticleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7053/api/");
        }

        // GET: Lấy danh sách phân trang
        public async Task<PagingResponse<NewsArticleDto>> GetListPagingAsync(NewsArticleSearchDto searchDto)
        {
            try
            {
                // Build query string
                var queryParams = new List<string>();
                if (searchDto.PageIndex > 0)
                    queryParams.Add($"PageIndex={searchDto.PageIndex}");
                if (searchDto.PageSize > 0)
                    queryParams.Add($"PageSize={searchDto.PageSize}");
                if (!string.IsNullOrEmpty(searchDto.Keyword))
                    queryParams.Add($"Keyword={Uri.EscapeDataString(searchDto.Keyword)}");
                if (searchDto.CategoryId.HasValue && searchDto.CategoryId > 0)
                    queryParams.Add($"CategoryId={searchDto.CategoryId}");

                var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

                var response = await _httpClient.GetAsync($"NewsArticles{queryString}");

                if (!response.IsSuccessStatusCode)
                {
                    return new PagingResponse<NewsArticleDto>();
                }

                var apiResponse = await response.Content
                    .ReadFromJsonAsync<PagingResponse<NewsArticleDto>>();

                return apiResponse ?? new PagingResponse<NewsArticleDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetListPagingAsync: {ex.Message}");
                return new PagingResponse<NewsArticleDto>();
            }
        }

        // GET: Lấy tất cả (không phân trang) - nếu cần
        public async Task<List<NewsArticleDto>> GetAllAsync()
        {
            try
            {
                var searchDto = new NewsArticleSearchDto
                {
                    PageIndex = 1,
                    PageSize = 1000 // Lấy nhiều để có tất cả
                };

                var result = await GetListPagingAsync(searchDto);
                return result.Items?.ToList() ?? new List<NewsArticleDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllAsync: {ex.Message}");
                return new List<NewsArticleDto>();
            }
        }

        // GET: Lấy theo ID
        public async Task<NewsArticleDto?> GetByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"NewsArticles/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var apiResponse = await response.Content
                    .ReadFromJsonAsync<NewsArticleDto>();

                return apiResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByIdAsync: {ex.Message}");
                return null;
            }
        }

        // POST: Tạo mới hoặc cập nhật
        public async Task<(bool Success, string Message, NewsArticleDto? Data)> CreateOrEditAsync(NewsArticleSaveDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("NewsArticles/create-or-edit", dto);

                var apiResponse = await response.Content
                    .ReadFromJsonAsync<ApiResponse<NewsArticleDto>>();

                if (apiResponse == null)
                {
                    return (false, "Failed to process response", null);
                }

                return (apiResponse.Success, apiResponse.Message ?? "", apiResponse.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateOrEditAsync: {ex.Message}");
                return (false, $"Error: {ex.Message}", null);
            }
        }

        // DELETE: Xóa news article
        public async Task<(bool Success, string Message)> DeleteAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"NewsArticles/{id}");

                var apiResponse = await response.Content
                    .ReadFromJsonAsync<ApiResponse<bool>>();

                if (apiResponse == null)
                {
                    return (false, "Failed to process response");
                }

                return (apiResponse.Success, apiResponse.Message ?? "");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAsync: {ex.Message}");
                return (false, $"Error: {ex.Message}");
            }
        }

        // POST: Thêm tag vào news article
        public async Task<(bool Success, string Message)> AddTagAsync(string newsArticleId, int tagId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"NewsArticles/{newsArticleId}/tags/{tagId}", null);

                var apiResponse = await response.Content
                    .ReadFromJsonAsync<ApiResponse<bool>>();

                if (apiResponse == null)
                {
                    return (false, "Failed to process response");
                }

                return (apiResponse.Success, apiResponse.Message ?? "Tag added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddTagAsync: {ex.Message}");
                return (false, $"Error: {ex.Message}");
            }
        }

        // DELETE: Xóa tag khỏi news article
        public async Task<(bool Success, string Message)> RemoveTagAsync(string newsArticleId, int tagId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"NewsArticles/{newsArticleId}/tags/{tagId}");

                var apiResponse = await response.Content
                    .ReadFromJsonAsync<ApiResponse<bool>>();

                if (apiResponse == null)
                {
                    return (false, "Failed to process response");
                }

                return (apiResponse.Success, apiResponse.Message ?? "Tag removed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RemoveTagAsync: {ex.Message}");
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
