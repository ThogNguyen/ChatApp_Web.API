using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp_Web.API.Data
{
    public class Group
    {
        public Guid GroupId { get; set; } // Khóa chính
        public string? GroupName { get; set; } // Tên nhóm

        public string? CreatedByUserId { get; set; } // Khóa ngoại tham chiếu đến bảng AspNetUsers trong Identity
        [ForeignKey(nameof(CreatedByUserId))]
        public IdentityUser? CreatedByUser { get; set; } // Người tạo nhóm

        public ICollection<GroupMember>? GroupMembers { get; set; } // Danh sách thành viên nhóm
        public ICollection<Message>? Messages { get; set; } // Danh sách tin nhắn trong nhóm
    }
}
