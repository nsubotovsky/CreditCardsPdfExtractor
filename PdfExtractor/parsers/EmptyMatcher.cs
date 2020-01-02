using PdfExtractor.parsers.Transactions;

namespace PdfExtractor.parsers
{
    public class EmptyMatcher : ITransactionFromLine
    {
        public (bool Success, Transaction transaction) GetFromLine(string line)
        {
            return (true, Transaction.EmtpyTransaction);
        }
    }
}