using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Reservations.Queries
{
    public interface IGetAllReservationService
    {
        Task<ResultDto<List<ResponseReservationDto>>> Execute();
    }
    public class GetAllReservationService : IGetAllReservationService
    {
        private readonly IMongoRepository<Reservation> _reservationRepository;
        public GetAllReservationService(IMongoRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<ResultDto<List<ResponseReservationDto>>> Execute()
        {
            var reservation = _reservationRepository.GetAllAsync().Result.Select(o => new ResponseReservationDto
            {
                Id = o.Id,
                StartDateTime = o.StartDateTime,
                EndDateTime = o.EndDateTime,
                PersonFullName = o.Person.FullName,
                Description=o.Description,
                ReservationNumber=o.ReservationNumber
            }).ToList();
            return new ResultDto<List<ResponseReservationDto>>()
            {
                Data = reservation,
                IsSuccess = true,
                Message = Messages.GetSuccess
            };
        }
    }
}
