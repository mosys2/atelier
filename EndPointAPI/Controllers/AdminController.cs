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
    public class AdminController : ControllerBase
    {
        private readonly IAddAdminService _adminService;
        private readonly IEditAdminService _editAdminService;
        private readonly IRemoveAdminService _removeAdminService;
        public AdminController
        (IAddAdminService adminService,
        IEditAdminService editAdminService,
        IRemoveAdminService removeAdminService
         )
        {
            _adminService = adminService;
            _editAdminService = editAdminService;
            _removeAdminService = removeAdminService;
        }
      
        // POST api/<BigAdminController>
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post(AddAdminDto addAdmin)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _adminService.Execute(addAdmin);
            return Ok(result);
        }
        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put(string id, [FromBody] EditAdminDto editAdmin)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var editByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result = await _editAdminService.Execute(id, editByUserId,editAdmin);
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
            var remByUserId= User.Claims.First(u => u.Type == "UserId").Value;
            var result=await _removeAdminService.Execute(id, remByUserId);
            return Ok(result);
        }
    }
}
