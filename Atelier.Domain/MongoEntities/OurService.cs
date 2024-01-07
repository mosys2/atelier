using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.MongoEntities
{
    public class OurService: IEntity
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public string Title { get; set; }
        public double RawPrice { get; set; } = 0; //قیمت خام
        public double PriceWithProfit { get; set; } = 0;//قیمت با محاسبه سود
        public string? Description { get; set; }
        public int Unit { get; set; }//1-number 2minutes

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
