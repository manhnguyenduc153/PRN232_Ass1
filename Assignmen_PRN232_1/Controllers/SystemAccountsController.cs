using Assignmen_PRN232_1.Services.IServices;
using Assignmen_PRN232__.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Assignmen_PRN232_1.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemAccountsController : ControllerBase
    {
        private readonly ISystemAccountService _systemAccountService;

        public SystemAccountsController(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

        // GET api/systemaccounts
        [HttpGet]
        public async Task<IActionResult> GetListPaging([FromQuery] SystemAccountSearchDto dto)
        {
            var response = await _systemAccountService.GetListPagingAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // GET api/systemaccounts/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _systemAccountService.GetAllAsync();
            return StatusCode(response.StatusCode, response);
        }

        // GET api/systemaccounts/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(short id)
        {
            var response = await _systemAccountService.GetByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // POST api/systemaccounts/create-or-edit
        [HttpPost("create-or-edit")]
        public async Task<IActionResult> CreateOrEdit([FromBody] SystemAccountSaveDto dto)
        {
            var response = await _systemAccountService.CreateOrEditAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE api/systemaccounts/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(short id)
        {
            var response = await _systemAccountService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // POST api/systemaccounts/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SystemAccountLoginDto dto)
        {
            var response = await _systemAccountService.LoginAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
