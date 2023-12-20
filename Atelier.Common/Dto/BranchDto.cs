using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class RequestAddBranchDto
    {
        [Required]
        public string AtelierBaseId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Code { get; set; }
        public bool Status { get; set; }
        public string? StatusDescription { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
    public class AddBranchDto
    {
        public string AtelierBaseId { get; set; }
        public string? InsertByUserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Code { get; set; }
        public bool Status { get; set; } = true;
        public string? StatusDescription { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }

}
