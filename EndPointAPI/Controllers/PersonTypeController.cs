using Atelier.Application.Services.Jobs.Commands;
using Atelier.Application.Services.Jobs.Queries;
using Atelier.Application.Services.PersonTypes.Commands;
using Atelier.Application.Services.PersonTypes.Queries;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Azure.Core;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonTypeController : ControllerBase
    {
        private readonly IGetAllPersonTypeService _getAllPersonType ;
        private readonly IAddPersonTypeService _addPersonType;
        private readonly Guid userId;
        private readonly Guid branchId;
        public PersonTypeController(IGetAllPersonTypeService getAllPersonType, IAddPersonTypeService addPersonType, ClaimsPrincipal user)
        {
           _addPersonType = addPersonType;
            _getAllPersonType = getAllPersonType;
            userId = Guid.Parse(ClaimUtility.GetUserId(user) ?? "");
            branchId = Guid.Parse(ClaimUtility.GetBranchId(user) ?? "");
        }
        // GET: api/<PersonTypeController>
        [HttpGet]
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
            var result = await _getAllPersonType.Execute(branchId);
            return Ok(result);
        }

        // POST api/<PersonTypeController>
        [HttpPost]
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

            var result = await _addPersonType.Execute(request, userId, branchId);
            return Ok(result);
        } 
    }
}
