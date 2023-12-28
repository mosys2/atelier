using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OurServiceController : ControllerBase
    {
        private readonly IOurServiceFacad _ourService;
        private readonly Guid userId;
        private readonly Guid branchId;
        public OurServiceController(IOurServiceFacad ourService, ClaimsPrincipal user)
        {
            _ourService = ourService;
            userId = Guid.Parse(ClaimUtility.GetUserId(user) ?? "");
            branchId = Guid.Parse(ClaimUtility.GetBranchId(user) ?? "");
        }
        // GET: api/<OurServiceController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ourServices = await _ourService.GetOurServiceService.Execute();
            return Ok(ourServices);
        }

        // GET api/<OurServiceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OurServiceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestOurServiceDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = true,
                    Message=Messages.InvalidForm
                });
            }
            var result = await _ourService.AddOurServiceService.Execute(request, userId, branchId);
            return Ok(result);
        }

        // PUT api/<OurServiceController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RequestOurServiceDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultDto
                {
                    IsSuccess= false,
                    Message=Messages.InvalidForm
                });
            }
            var result = await _ourService.EditOurService.Execute(request, userId, branchId);
            return Ok(result);
        }

        // DELETE api/<OurServiceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result=await _ourService.RemoveOurServiceService.Execute(id, userId);
            return Ok(result);  
        }
    }
}
