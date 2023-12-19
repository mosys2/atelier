using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RequestBranchDto
    {
        public string AtelierBaseId { get; set; }
    }
    public class ResultBranchDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Code { get; set; }
        public bool Status { get; set; } = true;
        public string? StatusDescription { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool isExpier { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string AtelierBaseId { get; set; }


    }

}
