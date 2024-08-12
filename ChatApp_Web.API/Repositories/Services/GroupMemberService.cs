using ChatApp_Web.API.Data;
using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.GroupMembersModels;
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

        public async Task<BaseResponse> AddMemberAsync(GroupMemberForCreate model)
        {
            // Check if the user exists
            var user = await db.Users
                .FirstOrDefaultAsync(u => u.UserName == model.Username);

            if (user == null)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Errors = "Người dùng không tồn tại."
                };
            }

            // Check if the user is already a member of the group
            var existingUser = await db.GroupMembers
                .AnyAsync(gm => gm.Group_Id == model.Group_Id
                                && gm.User_Id == user.Id);

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
                Group_Id = model.Group_Id,
                User_Id = user.Id,
                JoinedAt = model.JoinedAt
            };

            db.GroupMembers.Add(newMember);
            await db.SaveChangesAsync();

            return new BaseResponse
            {
                IsSuccess = true,
                Errors = "Thêm thành viên thành công."
            };
        }

        public async Task<GroupMemberInfo> GetGroupMemberInfoAsync(Guid groupId)
        {
            var group = await db.Groups
                .Where(g => g.GroupId == groupId)
                .Select(g => new GroupMemberInfo
                {
                    GroupName = g.GroupName,
                    MemberCount = db.GroupMembers.Count(gm => gm.Group_Id == groupId)
                })
                .FirstOrDefaultAsync();

            return group;
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
