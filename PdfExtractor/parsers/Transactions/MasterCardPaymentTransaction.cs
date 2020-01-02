using System.Globalization;
using System.Text.RegularExpressions;

namespace PdfExtractor.parsers.Transactions
{
    public class MasterCardPaymentTransaction : ITransactionFromLine
    {
        readonly Regex _payments = new Regex(@"(?<date>\d{2}-\w{3}-\d{2}) SU PAGO (?<currency>U\$S)?\s*(?<amount>[\d-,\.]+)", RegexOptions.Compiled);

        public (bool Success, Transaction transaction) GetFromLine(string line)
        {
            Match match = _payments.Match(line);
            if (!match.Success) return (false, Transaction.EmtpyTransaction);

            string currency = (string.IsNullOrWhiteSpace(match.Groups["currency"].Value)) ? "ars" : "usd";
            return (true, new Transaction()
            {
                Amount = decimal.Parse(match.Groups["amount"].Value, new CultureInfo("es-AR")),
                Date = DateParsers.ParseString(match.Groups["date"].Value),
                Currency = currency,
                Description = "SU PAGO" + (currency != "ars" ? " U$S" : ""),
                Who = "PAGO"
            });
        }
    }
}