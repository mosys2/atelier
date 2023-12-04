using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.Entities.Users
{
    public class User:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public int? Gender { get; set; } = 0;
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? ProfileImage { get; set; }
        //Base Entity
        public DateTime? InsertTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime? RemoveTime { get; set; }
    }
}
