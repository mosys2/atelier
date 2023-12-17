using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class AddSecretaryDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string BranchId { get; set; }
        public int? Gender { get; set; } = 0;
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? ProfileImage { get; set; }
        public string? HomeNumber { get; set; }
    }
}
