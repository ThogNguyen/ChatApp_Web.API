using ChatApp_Web.API.Data;
using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.GroupModels;
using ChatApp_Web.API.Models.Response;

namespace ChatApp_Web.API.Repositories.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupForView>> GetGroupsAsync();
        Task<GroupForView> GetGroupByIdAsync(Guid id);
        Task<BaseResponse> CreateGroupAsync(string userId ,GroupForCreate model);
    }
}
