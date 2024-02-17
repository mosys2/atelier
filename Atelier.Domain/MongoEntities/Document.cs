using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.MongoEntities
{
    public class Document:IEntity
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public long DocumentNumbr { get; set; }//شماره سند
        public int FinancialType {  get; set; }// نوع سند دریافتی یا پرداختی
        public int FactorType { get; set; }//نوع فاکتور قرارداد- متفرقه و ...
        public Person Person { get; set; }
        public List<DocumentItem>? documentItems { get; set; }


        //Common
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public Guid? InsertByUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? UpdateByUserId { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
        public Guid? RemoveByUserId { get; set; }
    }

    public class DocumentItem
    {
        public Guid Id { get; set; }
        public int TypePay { get; set; }//نقدی یا چک
        public Guid? ChequeId { get; set; }//اگر چک بود آی دی چک
        public string? TransactionNumber { get; set; }//اگر پرداخت نقدی بود شماره تراکنش
        public double Price { get; set; }

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
