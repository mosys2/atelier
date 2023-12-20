using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class GetAllRoleDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? PersianTitle { get; set; }
        public string? Description { get; set; }
    }

}
