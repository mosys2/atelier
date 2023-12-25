using Atelier.Application.Services.Roles.Queries.GetRoles;
using Atelier.Application.Services.TestMongo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IGetRolesService _getRolesService;
        public RoleController(IGetRolesService getRolesService)
        {
            _getRolesService = getRolesService;
        }
        // GET: api/<RoleController>
        [HttpGet]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get()
        {
            var result =await _getRolesService.Execute();
            return Ok(result);
        }
    }
}
