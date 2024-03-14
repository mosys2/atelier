using Atelier.Domain.Entities.Commons;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.Entities.Users
{
    public class Role: IdentityRole
    {
        public string? Description { get; set; }
        public string? PersianTitle { get; set; }
        //Base Entity
        public DateTime? InsertTime { get; set; } = DateTime.Now;
        public string? InsertByUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdateByUserId { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
        public string? RemoveByUserId { get; set; }
        public ICollection<RolePagePermission> RolePagePermissions { get; set; }

    }
    // Models/Page.cs
    public class Page: BaseEntity
    {
        public string Name { get; set; }
        // Navigation property for RolePagePermission
        public ICollection<RolePagePermission> RolePagePermissions { get; set; }
    }
    // Models/RolePagePermission.cs
    public class RolePagePermission:BaseEntity
    {
        // Foreign key properties
        public int RoleId { get; set; }
        public int PageId { get; set; }
        // Navigation properties
        public Role Role { get; set; }
        public Page Page { get; set; }
        public bool CanAccess { get; set; }
    }
}
