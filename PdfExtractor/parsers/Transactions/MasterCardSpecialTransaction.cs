using System.Text.RegularExpressions;

namespace PdfExtractor.parsers.Transactions
{
    public class MasterCardSpecialTransaction : ITransactionFromLine
    {
        private readonly Regex _specialTransaction = new Regex(@"^(?<date>\d{2}-\w{3}-\d{2})\s(?<description>.*?)(?<amount>[\d,\.-]+,\d\d)");

        public (bool Success, Transaction transaction) GetFromLine(string line)
        {
            Match match = _specialTransaction.Match(line);
            if (!match.Success) return (false, Transaction.EmtpyTransaction);

            Transaction transaction = TransactionParsers.ExtractTransactionFromMatch(match);
            transaction.Who = "OTROS";
            return (true, transaction);
        }
    }
}