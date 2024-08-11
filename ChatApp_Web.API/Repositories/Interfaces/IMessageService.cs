using ChatApp_Web.API.Models.Response;
using ChatApp_Web.API.Models;

namespace ChatApp_Web.API.Repositories.Interfaces
{
    public interface IMessageService
    {
        Task<BaseResponse> SendMessageAsync(MessageVM message);
        Task<List<MessageVM>> GetMessagesAsync(Guid groupId);
    }
}
