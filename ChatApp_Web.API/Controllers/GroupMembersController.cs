using ChatApp_Web.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> AddMemberAsync(Guid groupId, string userId)
        {
            var response = await _groupMemberService.AddMemberAsync(groupId, userId);
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
    }
}
