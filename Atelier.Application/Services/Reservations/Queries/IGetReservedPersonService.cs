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
    public interface IGetReservedPersonService
    {
        Task<ResultDto<List<ResponseDateReservation>>> Execute(Guid branchId,DateTime startDateTime, DateTime endDateTime);
    }
    public class GetReservedPersonService : IGetReservedPersonService
    {
        private readonly IMongoRepository<Reservation> _reservationRepository;
        public GetReservedPersonService( IMongoRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<ResultDto<List<ResponseDateReservation>>> Execute(Guid branchId, DateTime startDateTime, DateTime endDateTime)
        {
            var (dateReservation,total) =await _reservationRepository.GetAllAsync(w =>w.BranchId==branchId&&((w.StartDateTime <= startDateTime && w.EndDateTime >= endDateTime))||(w.StartDateTime<=endDateTime&&w.EndDateTime>=startDateTime), null);
           
                return new ResultDto<List<ResponseDateReservation>>
                {
                    Data = dateReservation.Select(e => new ResponseDateReservation
                    {
                        FullName = e.Person.FullName,
                        StartDateTime = e.StartDateTime,
                        EndDateTime = e.EndDateTime,
                        ReservationNumber = e.ReservationNumber
                    }).ToList(),
                    IsSuccess = true,
                    Message = Messages.GetSuccess
                };
        }
    }
}
