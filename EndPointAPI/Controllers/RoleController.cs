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
        private readonly ITestMongo _testMongo;
        public RoleController(IGetRolesService getRolesService,ITestMongo testMongo)
        {
            _getRolesService = getRolesService;
            _testMongo = testMongo;
        }
        // GET: api/<RoleController>
        [HttpGet]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get()
        {
            var res=await _testMongo.ExecuteAdd();
            var resut =await _getRolesService.Execute();
            return Ok(res);
        }
    }
}
