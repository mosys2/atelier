using Atelier.Domain.Entities.AtelierApp;
using Atelier.Domain.Entities.Commons;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public virtual Branch Branch { get; set; }
        public string BranchId { get; set; }
        public int? Gender { get; set; } = 0;
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? ProfileImage { get; set; }
        public string? HomeNumber { get; set; }
        //Base Entity
        public DateTime? InsertTime { get; set; } = DateTime.Now;
        public string? InsertByUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdateByUserId { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
        public string? RemoveByUserId { get; set; }
        public ICollection<JwtUserToken> JwtUserTokens { get; set; }

    }
    public class JwtUserToken:BaseEntity
    {
        public string TokenHash { get; set; }
        public DateTime TokenExp { get; set; }
        //public string MobileModel { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExp { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string BranchId { get; set; }

    }
}
