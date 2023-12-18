using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RequestAddBranchDto
    {

        public string AtelierBaseId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Code { get; set; }
        public bool Status { get; set; } = true;
        public string? StatusDescription { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
