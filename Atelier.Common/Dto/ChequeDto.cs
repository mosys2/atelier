using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RequestChequeDto
    {
        public Guid? Id { get; set; }
        [Required]
        public int FinancialType { get; set; }//نوع : پرداختی یا دریافت 1 و 2Enum
        [Required]
        public int StatusRegistered { get; set; }//ثبت شده یا نشده
        [Required]
        public string ChequeNumber { get; set; }
        [Required]
        public Guid BankId { get; set; }
        public string? AccountNumber { get; set; }//شماره حساب بانک
        public string? BankBranch { get; set; }//شعبه بانک
        [Required]
        public Guid PersonId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string? Phone { get; set; }
        [Required]
        public int StatusCheque { get; set; }//پاس شده یا نشده Enum
        public string? SpentInTheName { get; set; }//خرج شده به نام
        public string? Description { get; set; }
    }
    public class ResponseChequeDto
    {
        public Guid Id { get; set; }
        public string? FinancialType { get; set; }//نوع : پرداختی یا دریافت 1 و 2Enum
        public string? StatusRegistered { get; set; }//ثبت شده یا نشده
        public string? ChequeNumber { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }//شماره حساب بانک
        public string? BankBranch { get; set; }//شعبه بانک
        public string? PersonName { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string? Phone { get; set; }
        public string? StatusCheque { get; set; }//پاس شده یا نشده Enum
        public string? SpentInTheName { get; set; }//خرج شده به نام
        public string? Description { get; set; }
    }
}
