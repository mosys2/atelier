using Amazon.Auth.AccessControlPolicy;
using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Persons.FacadPattern;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
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
    
    public class ChequeController : ControllerBase
    {
        private readonly IChequeFacad _chequeFacad;
        private readonly Guid userId;
        private readonly Guid branchId;
        public ChequeController(IChequeFacad chequeFacad, ClaimsPrincipal user)
        {
            _chequeFacad = chequeFacad;
            userId = Guid.Parse(ClaimUtility.GetUserId(user) ?? "");
            branchId = Guid.Parse(ClaimUtility.GetBranchId(user) ?? "");
        }
        // GET: api/<ChequeController>
        [HttpGet]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get([FromQuery] int page, int pageSize = 20)
        {
            if (branchId == Guid.Empty)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotFoundUserOrBranch
                });
            }
            var result = await _chequeFacad.GetAllChequesService.Execute(branchId, new RequstPaginateDto { Page = page, PageSize = pageSize });
            return Ok(result);

        }
        // GET api/<ChequeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ChequeController>
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post([FromBody] RequestChequeDto requestCheque)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.InvalidForm
                });
            }
            var result = await _chequeFacad.AddChequeService.Execute(requestCheque, userId, branchId);
            return Ok(result);
        }

        // PUT api/<ChequeController>/5
        [HttpPut]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put([FromBody] RequestChequeDto requestCheque)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.InvalidForm
                });
            }
            var result = await _chequeFacad.EditChequeService.Execute(requestCheque, userId, branchId);
            return Ok(result);
        }

        // DELETE api/<ChequeController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _chequeFacad.RemoveChequeService.Execute(id, userId, branchId);
            return Ok(result);
        }
    }
}
