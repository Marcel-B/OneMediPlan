using System;
using OneMediPlan.Models;
using CoreText;
namespace OneMediPlan.Helpers
{
    public static class MediExtensions
    {
        public static string GetStockInfo(this Medi medi)
        {
            return $"{medi.Stock.ToString("F1")}/{medi.MinimumStock.ToString("F1")}";
        }

        public static string GetNextDate(this Medi medi)
        {
            if (medi.IntervallType == IntervallType.IfNedded)
                return "-";
            if (medi.NextDate == DateTimeOffset.MinValue)
                return "-";
            return medi.NextDate.ToString("dd.MM.y");
        }

        public static string GetLastDate(this Medi medi)
        {
            if (medi.LastDate == DateTimeOffset.MinValue)
                return "n/a";
            var now = DateTimeOffset.Now - medi.LastDate;
            if (now.Days == 0)
                return medi.LastDate.ToString("HH:mm");
            return medi.LastDate.ToString("dd.MM.y");
        }

        public static string GetDosage(this Medi medi){
            var type = "";
            switch (medi.DosageType)
            {
                case MediType.Fluency:
                   type =  "ml";
                    break;
                case MediType.Injection:
                    type = "Spritze(n)";
                    break;
                case MediType.Tablet:
                    type = "Tablette(n)";
                    break;
                default:
                    break;
            }
            return $"{medi.Dosage.ToString("F1")} {type}";
        }
    }
}
