using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.MongoEntities
{
    public class Contract : IEntity
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        //طرفین قرارداد
        public string? ExecutorContract { get; set; }//مجری قرارداد
        public string? UnitHead { get; set; }//مدیر مسئول واحد
        public string? UnitAddress { get; set; }
        public string? UnitPhone { get; set; }
        public string? UnitMobile { get; set; }
        public string? GuildNumber { get; set; }//شماره پروانه صنفی

        public string? ContractTitle { get; set; }//موضوع قرارداد
        public long ContractNumber { get; set; }//شماره قرارداد
        public DateTime ContractDate { get; set; }//تاریخ قرارداد
        public DateTime? CeremonyStartDateTime { get; set; }//تاریخ وساعت شروع مراسم
        public DateTime? CeremonyEndDateTime { get; set; }//تاریخ و ساعت پایان مراسم
        public string? CeremonyAddress { get; set; }//آدرس مراسم
        public string? Description { get; set; }


        public Person Person { get; set; }//طرف قرارداد
        public List<PaymentTerms>? PaymentTermsList { get; set; }//شرایط پرداخت لیست
        public List<ServiceContract>? ServiceContractList{ get; set; }//خدمات

        public double Discount { get; set; } = 0;
        public double Total { get; set; } = 0; //مبلغ کل

        //Common
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public Guid? InsertByUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? UpdateByUserId { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
        public Guid? RemoveByUserId { get; set; }
    }

    public class PaymentTerms
    {
        public Guid Id { get; set; }
        public int TypePay { get; set; }//1cash 2cheque
        public decimal Amount { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
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

    public class ServiceContract
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int Unit { get; set; }
        public double Value { get; set; }//اگر واحد دقیقه بود به ثانیه در اینجا 
        public double RawPrice { get; set; } = 0; //قیمت خام
        public double PriceWithProfit { get; set; } = 0;//قیمت با محاسبه سود
        public double Discont { get; set; } = 0;//تخفیف
        public double Total { get; set; } = 0; //مبلغ کل

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
