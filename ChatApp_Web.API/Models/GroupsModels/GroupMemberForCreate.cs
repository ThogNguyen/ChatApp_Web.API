namespace ChatApp_Web.API.Models.GroupsModels
{
    public class GroupMemberForCreate
    {
        public string? User_Id { get; set; }
        public Guid Group_Id { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.Now;
    }
}
