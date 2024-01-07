using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Jobs.Commands;
using Atelier.Application.Services.Jobs.Queries;
using Atelier.Application.Services.PersonTypes.Commands;
using Atelier.Application.Services.PersonTypes.Queries;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Azure.Core;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonTypeController : ControllerBase
    {
        private readonly IPersonTypeFacad _personTypeFacad;
        private readonly Guid userId;
        private readonly Guid branchId;

        public PersonTypeController(IPersonTypeFacad personTypeFacad, ClaimsPrincipal user)
        {
            _personTypeFacad = personTypeFacad;
            userId = Guid.Parse(ClaimUtility.GetUserId(user) ?? "");
            branchId = Guid.Parse(ClaimUtility.GetBranchId(user) ?? "");
        }
        // GET: api/<PersonTypeController>
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
            var result = await _personTypeFacad.GetAllPersonTypeService.Execute(branchId);
            return Ok(result);
        }

        // POST api/<PersonTypeController>
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post([FromBody] RequestPersonTypeDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.InvalidForm
                });
            }
            if (userId == Guid.Empty || branchId==Guid.Empty)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.NotFoundUserOrBranch
                });
            }

            var result = await _personTypeFacad.AddPersonTypeService.Execute(request, userId, branchId);
            return Ok(result);
        } 
    }
}
