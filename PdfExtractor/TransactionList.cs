using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace PdfExtractor
{
    public class TransactionList : List<Transaction>
    {
        public DateTime Cierre { get; set; }

        public string FormatDate(DateTime date)
        {
            string dateAsStr = date.ToString("dd-MMM-yy", new CultureInfo("es-AR"));
            return dateAsStr.Substring(0, 6) + dateAsStr.Substring(7);
        }

        public override string ToString()
        {
            List<string> lines = new List<string>();
            foreach (Transaction transaction in this)
            {
                string newLine = FormatDate(transaction.Date) + "\t" + transaction.Who + "\t" + transaction.Description + "\t";

                newLine = newLine + (transaction.Currency == "ars" ? "" : "\t") + (-transaction.Amount).ToString("F2");
                lines.Add(newLine);
            }

            lines.Add(FormatDate(Cierre) + "\t" + "Saldo Anterior" + "\t" + "Cierre" );

            return string.Join(Environment.NewLine, lines);
        }
    }
}