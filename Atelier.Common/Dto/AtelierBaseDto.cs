using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    

    public class ResultListAtelierBaseDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }
        public DateTime? InsertTime { get; set; }
        public int branchCount { get; set;}
    }
}
