using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RequestReservationDto
    {
        public Guid? Id { get; set; }
        [Required]
        public Guid PersonId { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        public string? Description { get; set; }
    }
    public class ResponseDateReservation
    {
        public string FullName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public long ReservationNumber { get; set; }
    }
    public class RequestDateTimeReservation
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
