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
    public interface IRemoveReservationService
    {
        Task<ResultDto> Execute(Guid id, Guid userId, Guid branchId);
    }
    public class RemoveReservationService : IRemoveReservationService
    {
        private readonly IMongoRepository<Reservation> _reservationRepository;
        public RemoveReservationService(IMongoRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<ResultDto> Execute(Guid id, Guid userId, Guid branchId)
        {
            using (var session = await _reservationRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    var reservation = await _reservationRepository.GetAsync(id,session);
                    if (reservation == null)
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = Messages.NotFind,
                        };
                    }

                    reservation.IsRemoved = true;
                    reservation.RemoveTime = DateTime.Now;
                    reservation.RemoveByUserId = userId;

                    await _reservationRepository.UpdateAsync(reservation,session);
                    await session.CommitTransactionAsync();
                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = Messages.Remove
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
