using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Auth;
using Atelier.Application.Services.Auth.Commands;
using Atelier.Application.Services.Users.Queries.FindRefreshToken;
using Atelier.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        public AuthenticationController(IUserFacad userFacad)
        {
           _userFacad=userFacad;
        }
       // POST api/<AuthenticationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestLoginDto request)
        {
            var result =await _userFacad.LoginService.Execute(request);
                return Ok(result);
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string Refreshtoken)
        {
            var result = await _userFacad.FindRefreshTokenService.Execute(Refreshtoken);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            var userId = User.Claims.First(p => p.Type == "UserId").Value;
            _userFacad.LogoutService.Execute(userId);
            return Ok();
        }
    }
}
