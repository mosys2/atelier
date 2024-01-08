using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RequestOurServiceDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Unit {  get; set; }
        public double RawPrice { get; set; }
        public double PriceWithProfit { get; set; } 
        public string? Description { get; set; }
    }
    public class ResponseOurServiceDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double RawPrice { get; set; }
        public double PriceWithProfit { get; set; }

    }
}
