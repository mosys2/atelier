using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{

    public class RequestBankDto
    {
        [Required]
        public string Name { get; set; }
       
    }
}
