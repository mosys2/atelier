using Atelier.Application.Services.Banks.Commands;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.ComponentModel;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IAddNewBankService _addNewBank;
       
        public BankController(IAddNewBankService addNewBank)
        {
            _addNewBank = addNewBank;
        }
        // GET: api/<BankController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok();
        }

        // GET api/<BankController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BankController>
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]

        public async Task<IActionResult> Post([FromBody] RequestBankDto request )
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.InvalidForm
                });
            }
            var userId =Guid.Parse(ClaimUtility.GetUserId(User)??"");
            var branchId =Guid.Parse(ClaimUtility.GetBranchId(User)??"");
            if (userId == Guid.Empty || branchId==Guid.Empty)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.NotFoundUserOrBranch
                });
            }
            var result = await _addNewBank.Execute(request, userId, branchId);
            return Ok(result);
        }

        // PUT api/<BankController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        // DELETE api/<BankController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}