using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp_Web.API.Data
{
    public class GroupMember
    {
        public Guid GroupMemberId { get; set; } // Khóa chính

        public string? User_Id { get; set; } // Khóa ngoại tham chiếu đến bảng AspNetUsers
        [ForeignKey(nameof(User_Id))]
        public IdentityUser? User { get; set; } // Người dùng

        public Guid Group_Id { get; set; } // Khóa ngoại tham chiếu đến Group
        [ForeignKey(nameof(Group_Id))]
        public Group? Group { get; set; } // Nhóm

        public DateTime JoinedAt { get; set; } // Ngày tham gia
    }
}
