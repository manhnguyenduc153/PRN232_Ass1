using Assignmen_PRN232_1.Services.IServices;
using Assignmen_PRN232__.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Assignmen_PRN232_1.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsArticlesController : ControllerBase
    {
        private readonly INewsArticleService _newsArticleService;

        public NewsArticlesController(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        // GET api/newsarticles
        [HttpGet]
        public async Task<IActionResult> GetListPaging([FromQuery] NewsArticleSearchDto dto)
        {
            var response = await _newsArticleService.GetListPagingAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // GET api/newsarticles/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _newsArticleService.GetAllAsync();
            return StatusCode(response.StatusCode, response);
        }

        // GET api/newsarticles/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _newsArticleService.GetByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // POST api/newsarticles/create-or-edit
        [HttpPost("create-or-edit")]
        public async Task<IActionResult> CreateOrEdit([FromBody] NewsArticleSaveDto dto)
        {
            var response = await _newsArticleService.CreateOrEditAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE api/newsarticles/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _newsArticleService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
