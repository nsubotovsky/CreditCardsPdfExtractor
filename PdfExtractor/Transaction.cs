using System;
using System.Runtime.CompilerServices;

namespace PdfExtractor
{
    public class Transaction
    {
        public static Transaction EmtpyTransaction { get; } = new Transaction() { IsEmpty = true};

        public Transaction()
        {
            this.IsEmpty = false;
        }

        public string Who { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public bool IsEmpty { get; private set; }


        public override string ToString()
        {
            return string.Format("Date : {1}{0}Money : {2}{0}Description : {3}{0}Location : {4}{0}Owner : {5}", Environment.NewLine, Date.ToString("dd-MMM-yyyy"), Amount, Description, Currency, Who);
        }
    }

}