using Atelier.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.Entities.AtelierApp
{
    public class AtelierBase : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = true;//برای قطع ارتباط کل سیستم ها
        public string? StatusMessage { get; set; }

        public ICollection<Branch> Branches { get; set; }
    }
}
