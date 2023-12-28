using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Persons.Commands;
using Atelier.Application.Services.Reservations.Commands;
using Atelier.Application.Services.Reservations.Queries;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Reservations.FacadPattern
{
    public class ReservationFacad : IReservationFacad
    {
        private readonly IMongoRepository<Person> _personRepository;
        private readonly IMongoRepository<Reservation> _reservationRepository;
        public ReservationFacad(IMongoRepository<Person> personRepository, IMongoRepository<Reservation> reservationRepository)
        {
            _personRepository = personRepository;
            _reservationRepository = reservationRepository;
        }
        private IAddReservationService _addReservationService;
        private IGetReservedPersonService _getReservedPersonService;
        private IEditReservationService _editReservationService;
        private IRemoveReservationService _removeReservationService;
        private IGetAllReservationService _getAllReservationService;
        public IAddReservationService AddReservationService
        {
            get
            {
                return _addReservationService = _addReservationService ?? new AddReservationService(_personRepository,_reservationRepository);
            }
        }

        public IGetReservedPersonService GetReservedPersonService
        {
            get
            {
                return _getReservedPersonService = _getReservedPersonService ?? new GetReservedPersonService(_reservationRepository);
            }
        }

        public IEditReservationService EditReservationService
        {
            get
            {
                return _editReservationService = _editReservationService ?? new EditReservationService(_personRepository, _reservationRepository);
            }
        }

        public IRemoveReservationService RemoveReservationService
        {
            get
            {
                return _removeReservationService = _removeReservationService ?? new RemoveReservationService(_reservationRepository);
            }
        }

        public IGetAllReservationService GetAllReservationService
        {
            get
            {
                return _getAllReservationService = _getAllReservationService ?? new GetAllReservationService(_reservationRepository);
            }
        }
    }
}
