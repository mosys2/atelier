using Atelier.Application.Services.Users.Commands.AddUser;
using Atelier.Application.Services.Users.Commands.DeleteUser;
using Atelier.Application.Services.Users.Commands.EditUser;
using Atelier.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretaryController : ControllerBase
    {
        private readonly IAddSecretaryService _addSecretaryService;
        private readonly IEditSecretaryService _editSecretaryService;
        private readonly IRemoveSecretaryService _removeSecretaryService;
        public SecretaryController(IAddSecretaryService addSecretaryService,IEditSecretaryService editSecretaryService,IRemoveSecretaryService removeSecretaryService)
        {
            _addSecretaryService=addSecretaryService;
            _editSecretaryService=editSecretaryService;
            _removeSecretaryService=removeSecretaryService;
        }
        // POST api/<SecretaryController>
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Post(AddSecretaryDto secretary)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result=await _addSecretaryService.Execute(secretary);
            return Ok(result);
        }

        // PUT api/<SecretaryController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Put(string id, [FromBody] EditSecretaryDto editSecretary)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var editByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result = await _editSecretaryService.Execute(id, editByUserId, editSecretary);
            return Ok(result);
        }

        // DELETE api/<SecretaryController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var remByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result =await _removeSecretaryService.Execute(id, remByUserId);
            return Ok(result);
        }
    }
}
