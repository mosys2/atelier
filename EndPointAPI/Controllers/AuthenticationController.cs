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
        private readonly ILoginService _loginService;
        private readonly IFindRefreshTokenService _findRefreshTokenService;
        private readonly ILogoutService _logoutService;
        public AuthenticationController(ILoginService loginService,IFindRefreshTokenService findRefreshTokenService,ILogoutService logoutService)
        {
            _loginService = loginService;
            _findRefreshTokenService = findRefreshTokenService;
            _logoutService = logoutService;
        }
       // POST api/<AuthenticationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestLoginDto request)
        {
            var result =await _loginService.Execute(request);
                return Ok(result);
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string Refreshtoken)
        {
            var result = await _findRefreshTokenService.Execute(Refreshtoken);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            var user = User.Claims.First(p => p.Type == "UserId").Value;
            _logoutService.Execute(user);
            return Ok();
        }
    }
}
