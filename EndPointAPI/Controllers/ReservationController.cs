using Amazon.Auth.AccessControlPolicy;
using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Persons.FacadPattern;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationFacad _reservationFacad;
        private readonly Guid userId;
        private readonly Guid branchId;
        public ReservationController(IReservationFacad reservationFacad, ClaimsPrincipal user)
        {
            _reservationFacad = reservationFacad;
            userId = Guid.Parse(ClaimUtility.GetUserId(user) ?? "");
            branchId = Guid.Parse(ClaimUtility.GetBranchId(user) ?? "");
        }
        // GET: api/<ReservationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReservationController>/5
        [HttpGet("GetConflictReservation")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult>  Get([FromBody] RequestDateTimeReservation request)
        {
            var result =await _reservationFacad.GetReservedPersonService.Execute(branchId, request.StartDateTime, request.EndDateTime);
            return Ok(result);
        }
        // POST api/<ReservationController>
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post([FromBody] RequestReservationDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.InvalidForm
                });
            }
            var result = await _reservationFacad.AddReservationService.Execute(request, userId, branchId);
            return Ok(result);
        }
        // PUT api/<ReservationController>/5
        [HttpPut]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put([FromBody] RequestReservationDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.InvalidForm
                });
            }
            var result = await _reservationFacad.EditReservationService.Execute(request, userId, branchId);
            return Ok(result);
        }
        // DELETE api/<ReservationController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _reservationFacad.RemoveReservationService.Execute(id, userId, branchId);
            return Ok(result);
        }
    }
}
