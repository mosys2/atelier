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
    public interface IEditReservationService
    {
        Task<ResultDto> Execute(RequestReservationDto request, Guid userId, Guid branchId);
    }
    public class EditReservationService : IEditReservationService
    {
        private readonly IMongoRepository<Person> _personRepository;
        private readonly IMongoRepository<Reservation> _reservationRepository;
        public EditReservationService(IMongoRepository<Person> personRepository, IMongoRepository<Reservation> reservationRepository)
        {
            _personRepository = personRepository;
            _reservationRepository = reservationRepository;
        }
        public async Task<ResultDto> Execute(RequestReservationDto request, Guid userId, Guid branchId)
        {
            using (var session = await _reservationRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    if (request.Id == null)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.NotFind
                        };
                    }
                    var currentReservation = await _reservationRepository.GetAsync(r => r.BranchId == branchId && r.Id == request.Id.Value, session);
                    if (currentReservation == null)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.NotFind,
                        };
                    }
                    var person = await _personRepository.GetAsync(p => p.BranchId == branchId && p.Id == request.PersonId, session);
                    if (person == null)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.PersonNotFound
                        };
                    }
                    if (request.StartDateTime > request.EndDateTime)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.INVAILD_DATETIME
                        };
                    }

                    currentReservation.BranchId = branchId;
                    currentReservation.UpdateByUserId = userId;
                    currentReservation.UpdateTime = DateTime.Now;
                    currentReservation.Person = person;
                    currentReservation.Description = request.Description;
                    currentReservation.PhoneNumber = request.PhoneNumber;
                    currentReservation.StartDateTime = request.StartDateTime;
                    currentReservation.EndDateTime = request.EndDateTime;

                    await _reservationRepository.UpdateAsync(currentReservation,session);
                    await session.CommitTransactionAsync();
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = Messages.MessageUpdate
                    };
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    Console.WriteLine(ex.Message);
                    return new ResultDto { IsSuccess = false, Message = Messages.FAILED_OPERATION };
                }
            }
        }
    }
}
