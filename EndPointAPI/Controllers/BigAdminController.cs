﻿using Atelier.Application.Services.Users.Commands.AddUser;
using Atelier.Application.Services.Users.Commands.DeleteUser;
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
        private readonly IRemoveBigAdminService _removeBigAdmin;
        public BigAdminController(IAddBigAdminService addBigAdmin,IRemoveBigAdminService removeBigAdminService)
        {
            _addBigAdmin=addBigAdmin;
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
        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Delete(string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result=await _removeBigAdmin.Execute(id);
            return Ok(result);
        }
    }
}
