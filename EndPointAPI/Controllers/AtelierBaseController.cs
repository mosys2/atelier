using Atelier.Application.Services.Ateliers.Commands;
using Atelier.Application.Services.Ateliers.Queries;
using Atelier.Common.Dto;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtelierBaseController : ControllerBase
    {
        private readonly IGetAllAtelierBase _getAllAtelierBase;
        private readonly IAddAtelierService _addAtelierService;
        public AtelierBaseController
        (IGetAllAtelierBase getAllAtelierBase,
            IAddAtelierService addAtelierService
        )
        {
            _getAllAtelierBase=getAllAtelierBase;
            _addAtelierService=addAtelierService;
        }
       
        // GET: api/<AtelierBaseController>
        [HttpGet]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get()
        {
            var result = await _getAllAtelierBase.Excute();
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post(RequestAddAtelierDto atelier)
        {
            if (!ModelState.IsValid)
            {
            return BadRequest(ModelState);
            }
            var userId = User.Claims.First(u => u.Type == "UserId").Value;
            var result = await _addAtelierService.Execute
                (
                new AddAtelierDto
                {
                    CurrentUserId=userId,
                    Description=atelier.Description,
                    Name=atelier.Name,
                    Status = atelier.Status,
                    StatusMessage = atelier.StatusMessage
                }
                );
            return Ok(result);
        }

    }
}
