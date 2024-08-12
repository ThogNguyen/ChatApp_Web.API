using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.GroupMembersModels;
using ChatApp_Web.API.Models.Response;

namespace ChatApp_Web.API.Repositories.Interfaces
{
    public interface IGroupMemberService
    {
        Task<GroupMemberInfo> GetGroupMemberInfoAsync(Guid groupId);
        Task<BaseResponse> AddMemberAsync(GroupMemberForCreate model);
        Task<List<GroupMemberForView>> GetMembersAsync(Guid groupId);
        Task<List<GroupMemberForView>> GetUserGroupsAsync(string userId);
    }
}
