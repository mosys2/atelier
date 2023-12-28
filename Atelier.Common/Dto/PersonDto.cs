using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RequestPersonDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Family { get; set; }
        public string? FullName { get; set; }
        public Guid? PersonTypeId { get; set; }
        public string? NationalCode { get; set; }
        public Guid? JobId { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Description { get; set; }
    }
    public class ResponsePersonDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? PersonTypeTitle { get; set; }
        public string? NationalCode { get; set; }
        public string? JobTitle { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
    }
}
