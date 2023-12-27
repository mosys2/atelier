using Atelier.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.MongoEntities
{
    public class Cheque : IEntity
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public int FinancialType { get; set; }//نوع : پرداختی یا دریافت 1 و 2Enum
        public int StatusRegistered { get; set; }//ثبت شده یا نشده
        public Bank Bank { get; set; }
        public Guid? Branch { get; set; }//شعبه بانک
        public Guid UserId { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string? Phone { get; set; }
        public int StatusCheque { get; set; }//پاس شده یا نشده Enum
        public Guid? SpentInTheName { get; set; }//خرج شده به نام
        public string? Description { get; set; }
        public string? Underwriter { get; set; }//پشت نویس

        //Common
        public DateTime? InsertTime { get; set; } = DateTime.Now;
        public Guid? InsertByUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? UpdateByUserId { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
        public Guid? RemoveByUserId { get; set; }

    }
}