using System.ComponentModel.DataAnnotations;

namespace ChatApp_Web.API.Models
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Email không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Email sai định dạng")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu không được bỏ trống")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string? ConfirmPassword { get; set; }
    }
}
