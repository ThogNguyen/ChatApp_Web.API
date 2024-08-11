using System.ComponentModel.DataAnnotations;

namespace ChatApp_Web.API.Models.GroupMembersModels
{
    public class GroupMemberForCreate
    {
        [Required(ErrorMessage = "Tên người dùng không được bỏ trống.")]
        public string? Username { get; set; }
        public Guid Group_Id { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.Now;
    }
}
