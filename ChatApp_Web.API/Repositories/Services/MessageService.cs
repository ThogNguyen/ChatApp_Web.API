using ChatApp_Web.API.Data;
using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.MesssageModels;
using ChatApp_Web.API.Models.Response;
using ChatApp_Web.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp_Web.API.Repositories.Services
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext db;

        public MessageService(AppDbContext dbContext)
        {
            db = dbContext;
        }
        public async Task<IEnumerable<MessageForView>> GetMessagesAsync(Guid groupId)
        {
            return await db.Messages
                .Where(m => m.Group_Id == groupId)
                .OrderByDescending(m => m.SentAt)
                .Select(m => new MessageForView
                {
                    MessageId = m.MessageId,
                    MessageContent = m.MessageContent,
                    Username = m.User!.UserName,
                    User_Id = m.User_Id,
                    Group_Id = m.Group_Id,
                    SentAt = m.SentAt
                })
                .ToListAsync();
        }

        public async Task<BaseResponse> SendMessageAsync(MessageForCreate message)
        {
            var newMessage = new Message
            {
                MessageContent = message.MessageContent,
                User_Id = message.User_Id,
                Group_Id = message.Group_Id,
                SentAt = DateTime.UtcNow
            };

            db.Messages.Add(newMessage);
            await db.SaveChangesAsync();

            return new BaseResponse
            {
                IsSuccess = true,
                Errors = "Tin nhắn đã được gửi thành công."
            };
        }
    }
}
