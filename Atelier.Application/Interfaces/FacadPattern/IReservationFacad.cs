using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Reservations.Commands;
using Atelier.Application.Services.Reservations.Queries;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IReservationFacad
    {
        IAddReservationService AddReservationService { get; }
        IGetReservedPersonService GetReservedPersonService { get; }
        IEditReservationService EditReservationService { get; }
        IRemoveReservationService RemoveReservationService { get; }
        IGetAllReservationService GetAllReservationService { get; }
    }
}
