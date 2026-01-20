using Assignmen_PRN232_1.Services.IServices;
using Assignmen_PRN232__.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Assignmen_PRN232_1.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET api/tags
        [HttpGet]
        public async Task<IActionResult> GetListPaging([FromQuery] TagSearchDto dto)
        {
            var response = await _tagService.GetListPagingAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // GET api/tags/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _tagService.GetByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // POST api/tags/create-or-edit
        [HttpPost("create-or-edit")]
        public async Task<IActionResult> CreateOrEdit([FromBody] TagSaveDto dto)
        {
            var response = await _tagService.CreateOrEditAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE api/tags/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _tagService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
