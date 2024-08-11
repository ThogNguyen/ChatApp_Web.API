using ChatApp_Web.API.Data;
using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.GroupModels;
using ChatApp_Web.API.Models.Response;
using ChatApp_Web.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp_Web.API.Repositories.Services
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext db;
        private readonly UserManager<IdentityUser> _userManager;

        public GroupService(AppDbContext context ,UserManager<IdentityUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        public async Task<BaseResponse> CreateGroupAsync(string userId, GroupForCreate model)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new BaseResponse ()
                {
                    IsSuccess = false,
                    Errors = "Người dùng không tồn tại."
                };
            }

            var group = new Group()
            {
                GroupId = Guid.NewGuid(),
                GroupName = model.GroupName,
                CreatedByUserId = userId
            };

            await db.Groups.AddAsync(group);
            await db.SaveChangesAsync();

            // Thêm người dùng vào nhóm
            var groupMember = new GroupMember
            {
                Group_Id = group.GroupId,
                User_Id = userId,
                JoinedAt = DateTime.Now
            };

            await db.GroupMembers.AddAsync(groupMember);
            await db.SaveChangesAsync();

            return new BaseResponse()
            {
                IsSuccess = true,
                Errors = "Tạo nhóm thành công.",
            };
        }

        public async Task<GroupForView> GetGroupByIdAsync(Guid id)
        {
            var group = await db.Groups.FindAsync(id);
            if (group != null)
            {
                return new GroupForView
                {
                    GroupName = group.GroupName
                };
            }

            return null;
        }

        public async Task<IEnumerable<GroupForView>> GetGroupsAsync()
        {
            var groups = await db.Groups.ToListAsync();

            return groups.Select(group => new GroupForView
            {
                GroupName = group.GroupName,
                CreatedByUserId = group.CreatedByUserId
            });
        }
    }
}
