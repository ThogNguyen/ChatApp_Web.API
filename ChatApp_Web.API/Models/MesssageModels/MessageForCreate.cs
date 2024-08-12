using System.ComponentModel.DataAnnotations;

namespace ChatApp_Web.API.Models.MesssageModels
{
    public class MessageForCreate
    {
        [Required(ErrorMessage = "Tin nhắn không được trống.")]
        public string? MessageContent { get; set; }
        public string? User_Id { get; set; }
        public Guid Group_Id { get; set; }
    }
}
