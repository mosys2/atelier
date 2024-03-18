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

    }
    // Models/Page.cs
    public class Page: BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public ICollection<PageAccess> PageAccess { get; set; }
    }
    public class PageAccess: BaseEntity
    {
        public string PageId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Page Page { get; set; }
        public bool CanAccess { get; set; } = false;

        // سایر خصوصیات
    }
}
