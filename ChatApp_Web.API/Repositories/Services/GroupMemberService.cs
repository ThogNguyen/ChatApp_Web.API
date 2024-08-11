using ChatApp_Web.API.Data;
using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.GroupsModels;
using ChatApp_Web.API.Models.Response;
using ChatApp_Web.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp_Web.API.Repositories.Services
{
    public class GroupMemberService : IGroupMemberService
    {
        private readonly AppDbContext db;

        public GroupMemberService(AppDbContext context)
        {
            db = context;
        }

        public async Task<BaseResponse> AddMemberAsync(Guid groupId, string userId)
        {
            // check user có trong nhóm ch
            var existingUser = await db.GroupMembers
                .AnyAsync(gm => gm.Group_Id == groupId
                                && gm.User_Id == userId);

            if (existingUser)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Errors = "Người dùng đã là thành viên của nhóm."
                };
            }

            var newMember = new GroupMember
            {
                Group_Id = groupId,
                User_Id = userId,
                JoinedAt = DateTime.Now
            };

            db.GroupMembers.Add(newMember);
            await db.SaveChangesAsync();

            return new BaseResponse
            {
                IsSuccess = true,
                Errors = "Thêm thành viên thành công."
            };
        }

        public async Task<List<GroupMemberForView>> GetMembersAsync(Guid groupId)
        {
            var members = await db.GroupMembers
                .Where(gm => gm.Group_Id == groupId)
                .Include(gm => gm.Group) 
                .Include(gm => gm.User)
                .Select(gm => new GroupMemberForView
                {
                    User_Id = gm.User_Id,
                    UserName = gm.User!.UserName,
                    Group_Id = gm.Group_Id,
                    GroupName = gm.Group!.GroupName,  
                    JoinedAt = gm.JoinedAt
                })
                .ToListAsync();

            return members;
        }

        public async Task<List<GroupMemberForView>> GetUserGroupsAsync(string userId)
        {
            var groups = await db.GroupMembers
                .Where(gm => gm.User_Id == userId)
                .Include(gm => gm.Group)
                .Include(gm => gm.User)
                .Select(gm => new GroupMemberForView
                {
                    User_Id = gm.User_Id,
                    UserName = gm.User!.UserName,
                    Group_Id = gm.Group_Id,
                    GroupName = gm.Group!.GroupName, 
                    JoinedAt = gm.JoinedAt
                })
                .ToListAsync();

            return groups;
        }
    }
}
