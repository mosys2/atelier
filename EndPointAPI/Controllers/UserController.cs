using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Users.Commands.ChangeStatusUser;
using Atelier.Application.Services.Users.Queries.GetAllUser;
using Atelier.Application.Services.Users.Queries.GetDetailsUser;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.AtelierApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        public UserController(IUserFacad userFacad)
        {
           _userFacad = userFacad;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get(string? roleId)
        {
            var result = await _userFacad.GetUsersService.Execute("",1,12,roleId);
            return Ok(result);
        }

        // GET api/<UsersController>/5
        [HttpGet("Detail/{userId}")]
        public async Task<IActionResult> GetDetail(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotFind
                });
            }
            var result=await _userFacad.GetDetailsUserService.Execute(userId);
            return Ok(result);
        }
        [HttpPost("ChangeStatus/{userId}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> ChangeStatus(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userFacad.ChangeStatusUserService.Execute(userId);
            return Ok(result);
        }
        [HttpPost("RequestChangePageAccessDto")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> AddPageAccess(RequestAddPageAccessDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userFacad.AddPageAccessService.Execute(request.userId,request.pageIds);
            return Ok(result);
        }
        [HttpGet("GetUserPageAccess")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> GetUserPageAccess()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userFacad.GetAllUserPageAccessService.Execute();
            return Ok(result);
        }
    }
}
