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
            var reservation = await _reservationRepository.GetAsync(id);
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

            await _reservationRepository.UpdateAsync(reservation);
            return new ResultDto()
            {
                IsSuccess = true,
                Message = Messages.Remove
            };
        }
    }
}
