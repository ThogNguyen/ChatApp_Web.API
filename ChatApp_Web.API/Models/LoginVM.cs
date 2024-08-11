using System.ComponentModel.DataAnnotations;

namespace ChatApp_Web.API.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string? Password { get; set; }
    }
}
