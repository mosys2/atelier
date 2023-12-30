using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Reservations.Commands
{
    public interface IAddReservationService
    {
        Task<ResultDto> Execute(RequestReservationDto request, Guid userId, Guid branchId);
    }
    public class AddReservationService : IAddReservationService
    {
        private readonly IMongoRepository<Person> _personRepository;
        private readonly IMongoRepository<Reservation> _reservationRepository;
        public AddReservationService(IMongoRepository<Person> personRepository, IMongoRepository<Reservation> reservationRepository)
        {
            _personRepository = personRepository;
            _reservationRepository = reservationRepository;
        }
        public async Task<ResultDto> Execute(RequestReservationDto request, Guid userId, Guid branchId)
        {
            var person=await _personRepository.GetAsync(p => p.BranchId == branchId && p.Id==request.PersonId);
            if(person==null)
            {
                return new ResultDto
                {
                    IsSuccess=false,
                    Message=Messages.PersonNotFound
                };
            }
            if(request.StartDateTime>request.EndDateTime)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.INVAILD_DATETIME
                };
            }
            var lastReservation =await _reservationRepository.GetLastAsync(o => o.BranchId == branchId);
            Reservation reservation=new Reservation()
            {
                BranchId = branchId,
                InsertByUserId=userId,
                InsertTime=DateTime.Now,
                ReservationNumber= lastReservation==null?100:lastReservation.ReservationNumber+1,
                Person =person,
                Description=request.Description,
                PhoneNumber=request.PhoneNumber,
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
            };
            await _reservationRepository.CreateAsync(reservation);
            return new ResultDto
            {
                IsSuccess=true,
                Message=Messages.MessageInsert
            };
        }
    }
}
