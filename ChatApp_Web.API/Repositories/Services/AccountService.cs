using ChatApp_Web.API.Data;
using ChatApp_Web.API.Helpers;
using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.Response;
using ChatApp_Web.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp_Web.API.Repositories.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTHandle _jwtHandle;

        public AccountService(AppDbContext context, UserManager<IdentityUser> userManager, JWTHandle jwtHandle)
        {
            db = context;
            _userManager = userManager;
            _jwtHandle = jwtHandle;
        }

        public async Task<LoginResponse> LoginAsync(LoginVM loginVM)
        {
            var user = await _userManager.FindByNameAsync(loginVM.Username!);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginVM.Password!))
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    Error = "Tên đăng nhập hoặc mật khẩu không đúng"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtHandle.CreateToken(user, roles);

            return new LoginResponse
            {
                IsSuccess = true,
                Token = token
            };
        }

        public async Task<BaseResponse> RegisterAsync(RegisterVM registerVM)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await db.Users
                .FirstOrDefaultAsync(u => u.Email == registerVM.Email);
            if (existingUser != null)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Errors = "Email đã được sử dụng."
                };
            }

            var user = new IdentityUser
            {
                UserName = registerVM.Username,
                Email = registerVM.Email
            };

            // Tạo người dùng mới và mã hóa mật khẩu
            var result = await _userManager.CreateAsync(user, registerVM.Password!);

            return new BaseResponse
            {
                IsSuccess = true,
                Errors = "Đăng kí thành công"
            };
        }
    }
}
