using Atelier.Application.Services.Jobs.Commands;
using Atelier.Application.Services.Jobs.Queries;
using Atelier.Application.Services.PersonTypes.Commands;
using Atelier.Application.Services.PersonTypes.Queries;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonTypeController : ControllerBase
    {
        private readonly IGetAllPersonTypeService _getAllPersonType ;
        private readonly IAddPersonTypeService _addPersonType;

        public PersonTypeController(IGetAllPersonTypeService getAllPersonType, IAddPersonTypeService addPersonType)
        {
           _addPersonType = addPersonType;
            _getAllPersonType = getAllPersonType;
        }
        // GET: api/<PersonTypeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Guid branchId = Guid.Parse(User.Claims.ToList()[1].Value ?? "");

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

            Guid userId = Guid.Parse(User.Claims.ToList()[0].Value ?? "");
            Guid branchId = Guid.Parse(User.Claims.ToList()[1].Value ?? "");

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
