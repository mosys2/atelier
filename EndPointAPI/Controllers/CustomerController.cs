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
    public class CustomerController : ControllerBase
    {
        private readonly IAddCustomerService _addcustomerService;
        private readonly IEditCustomerService _editcustomerService;
        private readonly IRemoveCustomerService _removecustomerService;
        public CustomerController(IAddCustomerService addCustomerService,IEditCustomerService editCustomer,IRemoveCustomerService removeCustomerService)
        {
            _addcustomerService = addCustomerService;
            _editcustomerService = editCustomer;
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

        // PUT api/<SecretaryController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put(string id, [FromBody] EditCustomerDto editCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var editByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result = await _editcustomerService.Execute(id, editByUserId, editCustomer);
            return Ok(result);
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
            var remByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result =await _removecustomerService.Execute(id, remByUserId);
            return Ok(result);
        }
    }
}
