using Atelier.Application.Services.Users.Commands.AddUser;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BigAdminController : ControllerBase
    {
        private readonly IAddBigAdminService _addBigAdmin; 
        public BigAdminController(IAddBigAdminService addBigAdmin)
        {
            _addBigAdmin=addBigAdmin;
        }


        // POST api/<BigAdminController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
          var result=await  _addBigAdmin.Execute();
          return Ok(result);
        }

        // PUT api/<BigAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BigAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
