using System.ComponentModel.DataAnnotations;

namespace ChatApp_Web.API.Models.GroupModels
{
    public class GroupForView
    {
        public string? GroupName { get; set; }
        public string? CreatedByUserId { get; set; }
    }
}
