using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Constants
{
    public static class TypePay
    {
        public const int Cash = 1;//نقدی
        public const int Cheque = 2;// چک

        public static List<int> TypePayList()
        {
            List<int> list = new List<int>();
            list.Add(Cash);
            list.Add(Cheque);
            return list;
        }
        public static string GetTitle(int id)
        {
            string title = "";
            switch (id)
            {
                case 1:
                    title= "نقد";
                    break;
                case 2:
                    title="چک";
                    break;
                default:
                    title= "نامشخص";
                    break;
            }
            return title;
        }
    }
    public static class ContractServiceUnit
    {
        public const int Number = 1;//عدد
        public const int Time = 2;// زمان برحسب ثانیه
        public static List<int> ContractServiceUnitList()
        {
            List<int> list = new List<int>();
            list.Add(Number);
            list.Add(Time);
            return list;
        }
        public static string GetTitle(int id)
        {
            string title = "";
            switch (id)
            {
                case 1:
                    title= "تعداد";
                    break;
                case 2:
                    title="زمان ثانیه";
                    break;
                default:
                    title= "نامشخص";
                    break;
            }
            return title;
        }
    }
    public static class FinancialType
    {
        public const int Payment = 1;//پرداختی
        public const int Receipt = 2;//دریافتی
        public static List<int> FinancialTypeList()
        {
            List<int> list = new List<int>();
            list.Add(Payment);
            list.Add(Receipt);
            return list;
        }
        public static string GetTitle(int id)
        {
            string title = "";
            switch (id)
            {
                case 1:
                    title= "پرداختی";
                    break;
                case 2:
                    title="دریافتی";
                    break;
                default:
                    title= "نامشخص";
                    break;
            }
            return title;
        }
    }

    public static class StatusCheque
    {
        public const int Passed = 1;//پاس شده
        public const int NotPassed = 2;//پاس نشده
        public static List<int> StatusChequeList()
        {
            List<int> list = new List<int>();
            list.Add(Passed);
            list.Add(NotPassed);
            return list;
        }
        public static string GetTitle(int id)
        {
            string title = "";
            switch (id)
            {
                case 1:
                    title= "پاس شده";
                    break;
                case 2:
                    title="پاس نشده";
                    break;
                default:
                    title= "نامشخص";
                    break;
            }
            return title;
        }
    }

    public static class StatusRegistered
    {
        public const int Registered = 1;//ثبت شده
        public const int NotRegistered = 2;//ثبت نشده
        public static List<int> StatusRegisteredList()
        {
            List<int> list = new List<int>();
            list.Add(Registered);
            list.Add(NotRegistered);
            return list;
        }
        public static string GetTitle(int id)
        {
            string title = "";
            switch (id)
            {
                case 1:
                    title= "ثبت شده";
                    break;
                case 2:
                    title="ثبت نشده";
                    break;
                default:
                    title= "نامشخص";
                    break;
            }
            return title;
        }
    }
}
