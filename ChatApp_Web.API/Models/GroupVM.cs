using System.ComponentModel.DataAnnotations;

namespace ChatApp_Web.API.Models
{
    public class GroupVM
    {
        [Required(ErrorMessage = "Tên nhóm không được bỏ trống.")]
        public string? GroupName { get; set; }
        public string? CreatedByUserId { get; set; }
    }
}
