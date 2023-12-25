using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Constants
{
    public enum FinancialType
    {
        payment=1,//پرداختی
        Receipt=2,//دریافتی
    }

    public enum StatusCheque
    {
        Passed=1,//پاس شده
        NotPassed=2//پاس نشده
    }

    public enum StatusRegistered
    {
        Registered = 1,//ثبت شده
        NotRegistered = 2//ثبت نشده
    }
}
