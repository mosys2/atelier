using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Persons.Commands;
using Atelier.Application.Services.Persons.Queries;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonFacad _personFacad;
        private readonly Guid userId;
        private readonly Guid branchId;
        public PersonController(IPersonFacad personFacad, ClaimsPrincipal user)
        {
            _personFacad=personFacad;
            userId = Guid.Parse(ClaimUtility.GetUserId(user) ?? "");
            branchId = Guid.Parse(ClaimUtility.GetBranchId(user) ?? "");
        }
        [HttpGet]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get()
        {
            if (branchId==Guid.Empty)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.NotFoundUserOrBranch
                });
            }
            var result = await _personFacad.GetAllPersonService.Execute(branchId);
            return Ok(result);

        }

        // POST api/<PersonController>
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post([FromBody] RequestPersonDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.InvalidForm
                });
            }
            var result = await _personFacad.AddPersonService.Execute(request, userId, branchId);
            return Ok(result);
        }
        [HttpPut]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put([FromBody] RequestPersonDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.InvalidForm
                });
            }
            var result = await _personFacad.EditPersonService.Execute(request, userId, branchId);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _personFacad.RemovePersonService.Execute(id,userId, branchId);
            return Ok(result);
        }
    }
}
