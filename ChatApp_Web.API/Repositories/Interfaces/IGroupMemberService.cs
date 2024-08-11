using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.GroupsModels;
using ChatApp_Web.API.Models.Response;

namespace ChatApp_Web.API.Repositories.Interfaces
{
    public interface IGroupMemberService
    {
        Task<BaseResponse> AddMemberAsync(Guid groupId, string userId);
        Task<List<GroupMemberForView>> GetMembersAsync(Guid groupId);
        Task<List<GroupMemberForView>> GetUserGroupsAsync(string userId);
    }
}
