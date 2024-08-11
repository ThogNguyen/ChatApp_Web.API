using ChatApp_Web.API.Data;
using ChatApp_Web.API.Models;
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

        public async Task<BaseResponse> CreateGroupAsync(GroupVM model)
        {
            var user = await _userManager.FindByIdAsync(model.CreatedByUserId!);
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
                CreatedByUserId = model.CreatedByUserId,
            };

            await db.Groups.AddAsync(group);
            await db.SaveChangesAsync();

            // Thêm người dùng vào nhóm
            var groupMember = new GroupMember
            {
                Group_Id = group.GroupId,
                User_Id = model.CreatedByUserId,
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

        public async Task<GroupVM> GetGroupByIdAsync(Guid id)
        {
            var group = await db.Groups.FindAsync(id);
            if (group != null)
            {
                return new GroupVM
                {
                    GroupName = group.GroupName,
                    CreatedByUserId = group.CreatedByUserId
                };
            }

            return null;
        }

        public async Task<IEnumerable<GroupVM>> GetGroupsAsync()
        {
            var groups = await db.Groups.ToListAsync();

            return groups.Select(group => new GroupVM
            {
                GroupName = group.GroupName,
                CreatedByUserId = group.CreatedByUserId
            });
        }
    }
}
