namespace ChatApp_Web.API.Models
{
    public class GroupMemberVM
    {
        public string? User_Id { get; set; }
        public Guid Group_Id { get; set; }
        public string? GroupName { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.Now;
    }
}
