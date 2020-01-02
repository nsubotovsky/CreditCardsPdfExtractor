using System.Globalization;
using System.Text.RegularExpressions;

namespace PdfExtractor.parsers.Transactions
{
    public class MasterCardIvaTransaction : ITransactionFromLine
    {
        private readonly Regex _ivaMatch = new Regex(@"(?<description>(I\.V\.A\. 21,0%|INTERESES DE FINANCIACION|IMPUESTO DE SELLOS)) (?<amount>\S+)", RegexOptions.Compiled);

        public (bool Success, Transaction transaction) GetFromLine(string line)
        {
            Match match = _ivaMatch.Match(line);
            if (!match.Success) return (false, Transaction.EmtpyTransaction);

            return (true, new Transaction()
            {
                Description = match.Groups["description"].Value,
                Currency = "ars",
                Amount = decimal.Parse(match.Groups["amount"].Value, new CultureInfo("es-AR")),
                Who = "OTROS",
            });
            
        }
    }
}