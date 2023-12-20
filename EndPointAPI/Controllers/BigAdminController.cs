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
    public class BigAdminController : ControllerBase
    {
        private readonly IAddBigAdminService _addBigAdmin;
        private readonly IEditBigAdminService _editBigAdmin;
        private readonly IRemoveBigAdminService _removeBigAdmin;
        public BigAdminController(IAddBigAdminService addBigAdmin,IEditBigAdminService editBigAdminService,IRemoveBigAdminService removeBigAdminService)
        {
            _addBigAdmin=addBigAdmin;
            _editBigAdmin=editBigAdminService;
            _removeBigAdmin=removeBigAdminService;
        }


        // POST api/<BigAdminController>
        [HttpPost]
        [Authorize(Policy ="BigAdmin")]
        public async Task<IActionResult> Post(AddBigAdminDto bigAdmin)
        {
            if(!ModelState.IsValid)
            {
            return BadRequest(ModelState);
            }
          var result=await  _addBigAdmin.Execute(bigAdmin);
          return Ok(result);
        }
        // PUT api/<SecretaryController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put(string id, [FromBody] EditBigAdminDto bigAdmin)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var editByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result=await _editBigAdmin.Execute(id,editByUserId,bigAdmin);
            return Ok(result);
        }
        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var remByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result =await _removeBigAdmin.Execute(id, remByUserId);
            return Ok(result);
        }
    }
}
