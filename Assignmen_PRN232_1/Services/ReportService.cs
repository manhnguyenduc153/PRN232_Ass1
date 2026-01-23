using Assignmen_PRN232__.Dto;
using Assignmen_PRN232__.Repositories.IRepositories;
using Assignmen_PRN232_1.Services.IServices;

namespace Assignmen_PRN232_1.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<NewsArticleReportDto> GetNewsArticleReportAsync(ReportSearchDto searchDto)
        {
            return await _reportRepository.GetNewsArticleReportAsync(searchDto);
        }
    }
}
