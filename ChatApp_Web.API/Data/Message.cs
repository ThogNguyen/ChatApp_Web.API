using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp_Web.API.Data
{
    public class Message
    {
        public Guid MessageId { get; set; } // Khóa chính
        public string? MessageContent { get; set; } // Nội dung tin nhắn

        public string? User_Id { get; set; } // Khóa ngoại tham chiếu đến bảng AspNetUsers
        [ForeignKey(nameof(User_Id))]
        public IdentityUser? User { get; set; } // Người gửi tin nhắn

        public Guid Group_Id { get; set; } // Khóa ngoại tham chiếu đến Group
        [ForeignKey(nameof(Group_Id))]
        public Group? Group { get; set; } // Nhóm chứa tin nhắn

        public DateTime SentAt { get; set; } // Ngày gửi
    }
}
