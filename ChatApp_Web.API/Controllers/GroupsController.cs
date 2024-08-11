using Azure.Core;
using ChatApp_Web.API.Models;
using ChatApp_Web.API.Models.Response;
using ChatApp_Web.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp_Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var groups = await _groupService.GetGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);

            if (group == null)
            {
                var errorResponse = new BaseResponse
                {
                    IsSuccess = false,
                    Errors = $"Group with ID {id} not found."
                };
                return NotFound(errorResponse);
            }

            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> Post(GroupVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _groupService.CreateGroupAsync(model);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
