using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class AddAdminDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string BranchId { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? ProfileImage { get; set; }
        public string? HomeNumber { get; set; }
    }
    public class EditAdminDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string BranchId { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? ProfileImage { get; set; }
        public string? HomeNumber { get; set; }
    }
    
}
