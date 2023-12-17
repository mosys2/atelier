using Atelier.Application.Services.Ateliers.Queries;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtelierBaseController : ControllerBase
    {
        private readonly IGetAllAtelierBase _getAllAtelierBase;
        public AtelierBaseController(IGetAllAtelierBase getAllAtelierBase)
        {
            _getAllAtelierBase=getAllAtelierBase;
        }
       
        // GET: api/<AtelierBaseController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _getAllAtelierBase.Excute();
            return Ok(result);
        }


    }
}
