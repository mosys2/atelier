using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RequestPersonTypeDto
    {
        [Required]
        public string Title { get; set; }
    }
    public class ResponsePersonTypeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

    }
}
