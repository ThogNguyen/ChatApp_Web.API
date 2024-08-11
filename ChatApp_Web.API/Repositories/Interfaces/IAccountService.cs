using ChatApp_Web.API.Models.Response;
using ChatApp_Web.API.Models;

namespace ChatApp_Web.API.Repositories.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse> RegisterAsync(RegisterVM registerVM);
        Task<LoginResponse> LoginAsync(LoginVM loginVM);
    }
}
