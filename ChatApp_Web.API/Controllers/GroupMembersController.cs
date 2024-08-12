using ChatApp_Web.API.Models.GroupMembersModels;
using ChatApp_Web.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp_Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupMembersController : ControllerBase
    {
        private readonly IGroupMemberService _groupMemberService;

        public GroupMembersController(IGroupMemberService groupMemberService)
        {
            _groupMemberService = groupMemberService;
        }

        // Thêm thành viên vào nhóm
        [HttpPost("add-people")]
        public async Task<IActionResult> AddMemberAsync(GroupMemberForCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _groupMemberService.AddMemberAsync(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Lấy danh sách thành viên của nhóm
        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetMembersAsync(Guid groupId)
        {
            var members = await _groupMemberService.GetMembersAsync(groupId);
            if (members != null)
            {
                return Ok(members);
            }
            return NotFound("Không tìm thấy thành viên nào.");
        }

        // Lấy danh sách nhóm của người dùng
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserGroupsAsync(string userId)
        {
            var groups = await _groupMemberService.GetUserGroupsAsync(userId);
            if (groups != null)
            {
                return Ok(groups);
            }
            return NotFound("Không tìm thấy nhóm nào.");
        }

        // Lấy thông tin nhóm (Tên nhóm và số thành viên)
        [HttpGet("info/{groupId}")]
        public async Task<IActionResult> GetGroupInfoAsync(Guid groupId)
        {
            var groupInfo = await _groupMemberService.GetGroupMemberInfoAsync(groupId);
            if (groupInfo != null)
            {
                return Ok(groupInfo);
            }
            return NotFound("Không tìm thấy thông tin nhóm.");
        }
    }
}
