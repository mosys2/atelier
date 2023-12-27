using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Constants
{
    public static class FinancialType
    {
        const int Payment = 1;//پرداختی
        const int Receipt = 2;//دریافتی
        public static List<int> FinancialTypeList()
        {
            List<int> list = new List<int>();
            list.Add(Payment);
            list.Add(Receipt);
            return list;
        }
    }

    public static class StatusCheque
    {
        const int Passed = 1;//پاس شده
        const int NotPassed = 2;//پاس نشده
        public static List<int> StatusChequeList()
        {
            List<int> list = new List<int>();
            list.Add(Passed);
            list.Add(NotPassed);
            return list;
        }
    }

    public static class StatusRegistered
    {
        const int Registered = 1;//ثبت شده
        const int NotRegistered = 2;//ثبت نشده
        public static List<int> StatusRegisteredList()
        {
            List<int> list = new List<int>();
            list.Add(Registered);
            list.Add(NotRegistered);
            return list;
        }
    }
}
