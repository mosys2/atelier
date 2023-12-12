using Atelier.Domain.Entities.Commons;
using Atelier.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.Entities.AtelierApp
{
    public class Branch:BaseEntity
    {
        public virtual AtelierBase AtelierBase { get; set; }
        public string AtelierBaseId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Code { get; set; }
        public bool Status { get; set; } = true;
        public string? StatusDescription { get; set; }
        public DateTime ExpireDate { get; set; }

        public ICollection<User> Users { get; set; }
            


    }
}
