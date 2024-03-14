using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RequestPageDto
    {
        [Required]
        public string   Name { get; set; }
    }
    public class EditPageDto
    {
        [Required]
        public string Name { get; set; }
    }
    public class ResultListPageDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime? InsertTime { get; set; }
    }
}
