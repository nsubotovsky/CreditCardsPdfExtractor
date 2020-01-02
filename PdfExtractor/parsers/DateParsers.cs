using System;
using System.Text.RegularExpressions;

namespace PdfExtractor.parsers
{
    public class DateParsers
    {
        private static int MonthMmMtoMm(string mmm)
        {
            switch (mmm.ToLower())
            {
                case "ene": return 1;
                case "feb": return 2;
                case "mar": return 3;
                case "abr": return 4;
                case "may": return 5;
                case "jun": return 6;
                case "jul": return 7;
                case "ago": return 8;
                case "sep": return 9;
                case "oct": return 10;
                case "nov": return 11;
                case "dic": return 12;
            }
            throw new ArgumentException();
        }

        public static DateTime ParseRegexMatch(Match dateMatch)
        {
            int year = 2000 + Int32.Parse(dateMatch.Groups["year"].Value);
            int month = MonthMmMtoMm(dateMatch.Groups["month"].Value);
            int day = Int32.Parse(dateMatch.Groups["day"].Value);
            return new DateTime(year, month, day);
        }

        public static DateTime ParseString(string date)
        {
            int day = Int32.Parse(date.Substring(0, 2));
            int month = DateParsers.MonthMmMtoMm(date.Substring(3, 3));
            int year = 2000 + Int32.Parse(date.Substring(7, 2));
            return new DateTime(year, month, day);
        }

        public static DateTime ExtractDateFromString(string date)
        {
            int day = Int32.Parse(date.Substring(0, 2));
            int month = Int32.Parse(date.Substring(3, 2));
            int year = 2000 + Int32.Parse(date.Substring(6, 2));
            return new DateTime(year, month, day);
        }
    }
}