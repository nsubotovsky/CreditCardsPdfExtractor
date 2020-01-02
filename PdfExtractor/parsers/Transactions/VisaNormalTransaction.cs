using System.Text.RegularExpressions;

namespace PdfExtractor.parsers.Transactions
{
    public class VisaNormalTransaction : ITransactionFromLine
    {
        readonly Regex _normalTransaction = new Regex(@"^\s{7}(?<date>\d\d\.\d\d\.\d\d).{16}(?<description>.{55})(?<amountInArs>.{18}\s+)(?<amountInUsd>.{18})", RegexOptions.Compiled);

        public (bool Success, Transaction transaction) GetFromLine(string line)
        {
            Match match = _normalTransaction.Match(line.PadRight(124));
            if (!match.Success) return (false, Transaction.EmtpyTransaction);
            return (true, VisaParser.ExtractTransactionFromMatch(match));
        }
    }
}