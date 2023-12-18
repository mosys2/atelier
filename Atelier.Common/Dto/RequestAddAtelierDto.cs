using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RequestAddAtelierDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
        public string? StatusMessage { get; set; }
    }
}
