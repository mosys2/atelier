using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class DetailAccessPageDto
    {
        public string NamePage { get; set; }
        public string? FullName { get; set; }
        public string Role { get; set; }
        public string Url { get; set; }

    }
    public class RequestPageDto
    {
        [Required]
        public string   Name { get; set; }
        [Required]
        public string Url { get; set; }

    }
    public class EditPageDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
    }
    public class ResultListPageDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime? InsertTime { get; set; }
    }
}
