using Atelier.Application.Services.Users.Queries.GetAllUser;
using Atelier.Application.Services.Users.Queries.GetDetailsUser;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.AtelierApp;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGetUsersService _usersService;
        private readonly IGetDetailsUserService _detailsUserService;
        public UserController(IGetUsersService getUsersService,IGetDetailsUserService getDetailsUserService)
        {
            _usersService = getUsersService;
            _detailsUserService = getDetailsUserService;
        }
        // GET: api/<UsersController>
        [HttpGet("ByRole/{roleId}")]
        public async Task<IActionResult> Get(string roleId)
        {
            var result = await _usersService.Execute("",1,12,roleId);
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
            var result=await _detailsUserService.Execute(userId);
            return Ok(result);
        }
    }
}
