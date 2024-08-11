using ChatApp_Web.API.Models;
using ChatApp_Web.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp_Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterVM registerVM)
        {
            // Kiểm tra tính hợp lệ 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Gọi phương thức RegisterAsync để đăng ký người dùng
            var result = await _accountService.RegisterAsync(registerVM);

            // Trả về kết quả
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginVM loginVM)
        {
            // Kiểm tra tính hợp lệ 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Gọi phương thức LoginAsync để đăng ký người dùng
            var result = await _accountService.LoginAsync(loginVM);

            // Trả về kết quả
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }
    }
}
