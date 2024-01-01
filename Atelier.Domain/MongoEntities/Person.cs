using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.MongoEntities
{
    public class Person:IEntity
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? FullName { get; set; }
        public PersonType? PersonType { get; set; }
        public string? NationalCode{ get; set; }
        public Job? Job { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Description { get; set; }

        //Common
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public Guid? InsertByUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? UpdateByUserId { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
        public Guid? RemoveByUserId { get; set; }

    }
}
