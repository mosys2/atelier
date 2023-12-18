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
    public class CustomerController : ControllerBase
    {
        private readonly IAddCustomerService _addcustomerService;
        private readonly IRemoveCustomerService _removecustomerService;
        public CustomerController(IAddCustomerService addCustomerService,IRemoveCustomerService removeCustomerService)
        {
            _addcustomerService = addCustomerService;
            _removecustomerService = removeCustomerService;
        }
        // POST api/<CustomerController>
        [HttpPost]
        [Authorize(Policy = "BigAdminOrAdminOrSecretary")]
        public async Task<IActionResult> Post(AddCustomerDto customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result =await _addcustomerService.Execute(customer);
            return Ok(result);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdminOrAdminOrSecretary")]
        public async Task<IActionResult> Delete(string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result=await _removecustomerService.Execute(id);
            return Ok(result);
        }
    }
}
