using System.ComponentModel.DataAnnotations;

namespace ChatApp_Web.API.Models.GroupModels
{
    public class GroupForCreate
    {
        [Required(ErrorMessage = "Tên nhóm không được bỏ trống.")]
        public string? GroupName { get; set; }
    }
}
