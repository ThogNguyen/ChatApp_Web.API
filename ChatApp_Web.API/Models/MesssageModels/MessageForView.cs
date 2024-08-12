namespace ChatApp_Web.API.Models.MesssageModels
{
    public class MessageForView
    {
        public Guid MessageId { get; set; }
        public string? Username { get; set; }
        public string? MessageContent { get; set; }
        public string? User_Id { get; set; }
        public Guid Group_Id { get; set; }
        public DateTime SentAt { get; set; }
    }
}
