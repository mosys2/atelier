using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Cheques.FacadPattern;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractFacad _contractFacad;
        private readonly Guid userId;
        private readonly Guid branchId;
        public ContractController(IContractFacad contractFacad, ClaimsPrincipal user)
        {
            _contractFacad=contractFacad;
            userId = Guid.Parse(ClaimUtility.GetUserId(user) ?? "");
            branchId = Guid.Parse(ClaimUtility.GetBranchId(user) ?? "");
        }
        // GET: api/<ContractController>
        [HttpGet]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get([FromQuery] int page, int pageSize = 20)
        {
            var result = await _contractFacad.GetAllContractService.Execute(branchId, new RequstPaginateDto { Page = page, PageSize = pageSize });
            return Ok(result);
        }

        // GET api/<ContractController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContractController>
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post([FromBody] RequestContractDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.InvalidForm
                });
            }
            var result=await _contractFacad.AddContractService.Execute(request, userId,branchId);
            return Ok(result);
        }

        // PUT api/<ContractController>/5
        [HttpPut]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put([FromBody] RequestContractDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.InvalidForm
                });
            }
            var result = await _contractFacad.EditServiceContract.Execute(request, userId, branchId);
            return Ok(result);
        }

        // DELETE api/<ContractController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdmin")]

        public async Task<IActionResult>Delete(Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return NotFound();
            }
            var result = await _contractFacad.RemoveContractService.Execute(id, userId,branchId);
            return Ok(result);
        }
    }
}
