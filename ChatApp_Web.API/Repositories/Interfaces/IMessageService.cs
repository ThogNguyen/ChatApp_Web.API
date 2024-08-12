using ChatApp_Web.API.Models.Response;
using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.MesssageModels;

namespace ChatApp_Web.API.Repositories.Interfaces
{
    public interface IMessageService
    {
        Task<BaseResponse> SendMessageAsync(MessageForCreate message);
        Task<IEnumerable<MessageForView>> GetMessagesAsync(Guid groupId);
    }
}
