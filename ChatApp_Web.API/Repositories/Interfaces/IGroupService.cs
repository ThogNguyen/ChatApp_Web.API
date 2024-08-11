using ChatApp_Web.API.Data;
using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.Response;

namespace ChatApp_Web.API.Repositories.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupVM>> GetGroupsAsync();
        Task<GroupVM> GetGroupByIdAsync(Guid id);
        Task<BaseResponse> CreateGroupAsync(GroupVM model);
    }
}
