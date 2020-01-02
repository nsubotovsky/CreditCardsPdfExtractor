using System.Text.RegularExpressions;

namespace PdfExtractor.parsers.Transactions
{
    public class MasterCardNormalTransaction : ITransactionFromLine
    {
        readonly Regex _normalTransaction = new Regex(@"(?<date>\d{2}-\w{3}-\d{2})\s(?<description>.*?)\s\d+\s(?<amount>[\d,\.-]+)$", RegexOptions.Compiled);
        public (bool Success, Transaction transaction) GetFromLine(string line)
        {
            Match match = _normalTransaction.Match(line);
            if (!match.Success) return (false, Transaction.EmtpyTransaction);
            return (true,TransactionParsers.ExtractTransactionFromMatch(match));
        }
    }
}