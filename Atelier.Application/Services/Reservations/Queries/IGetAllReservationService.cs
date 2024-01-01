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
        Task<ResultDto<List<ResponseReservationDto>>> Execute(Guid branchId, RequstPaginateDto pagination);
    }
    public class GetAllReservationService : IGetAllReservationService
    {
        private readonly IMongoRepository<Reservation> _reservationRepository;
        public GetAllReservationService(IMongoRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<ResultDto<List<ResponseReservationDto>>> Execute(Guid branchId, RequstPaginateDto pagination)
        {
            using (var session = await _reservationRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    var (reservation, total) = await _reservationRepository.GetAllAsync(q => q.BranchId == branchId, pagination,session);
                    var reservationList = reservation.Select(o => new ResponseReservationDto
                    {
                        Id = o.Id,
                        StartDateTime = o.StartDateTime,
                        EndDateTime = o.EndDateTime,
                        PersonFullName = o.Person.FullName,
                        Description = o.Description,
                        ReservationNumber = o.ReservationNumber
                    }).ToList();
                    await session.CommitTransactionAsync();
                    return new ResultDto<List<ResponseReservationDto>>()
                    {
                        Data = reservationList,
                        Total = total,
                        IsSuccess = true,
                        Message = Messages.GetSuccess
                    };
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    Console.WriteLine(ex.Message);
                    return new ResultDto<List<ResponseReservationDto>>() { IsSuccess = false, Message = Messages.FAILED_OPERATION };
                }
            }
        }
    }
}
