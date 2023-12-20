using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RegisterUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int Gender { get; set; } = 0;
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }     
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public string[] Roles { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class ResultUserDto
    {
        public List<GetUsersDto> Users { get; set; }
        public int TotalRow { get; set; }
    }
    public class GetUsersDto
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public int BranchCode { get; set; }
        public string BranchTitle { get; set; }
    }
   
    public class GetDetailUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string BranchTitle { get; set; }
        public int? Gender { get; set; } = 0;
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? ProfileImage { get; set; }
        public string? HomeNumber { get; set; }
    }
}
