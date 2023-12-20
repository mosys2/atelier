using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class RequestAddAtelierDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
        public string? StatusMessage { get; set; }
    }
    public class AddAtelierDto
    {
        public string? CurrentUserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public string? StatusMessage { get; set; }
    }
    public class GetDetailAtelierDto
    {
        public string AtelierId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? InsertTime { get; set; }
        public bool Status { get; set; }
        public string? StatusMessage { get; set; }
    }
}
