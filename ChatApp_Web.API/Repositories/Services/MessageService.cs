using ChatApp_Web.API.Data;
using ChatApp_Web.API.Models;
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
        public async Task<List<MessageVM>> GetMessagesAsync(Guid groupId)
        {
            return await db.Messages
                .Where(m => m.Group_Id == groupId)
                .Select(m => new MessageVM
                {
                    MessageContent = m.MessageContent,
                    User_Id = m.User_Id,
                    Group_Id = m.Group_Id,
                    SentAt = m.SentAt
                })
                .ToListAsync();
        }

        public async Task<BaseResponse> SendMessageAsync(MessageVM message)
        {
            var newMessage = new Message
            {
                MessageContent = message.MessageContent,
                User_Id = message.User_Id,
                Group_Id = message.Group_Id,
                SentAt = DateTime.Now
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
