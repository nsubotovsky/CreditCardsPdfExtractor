using System;
using PdfExtractor.parsers.Transactions;

namespace PdfExtractor.parsers
{
    public class TransactionMatcherAction
    {
        private readonly ITransactionFromLine _transactionFromLine;
        private readonly Action<Transaction> _whatNext;
        private Transaction _transaction;

        public TransactionMatcherAction(ITransactionFromLine transactionFromLine, Action<Transaction> whatNext)
        {
            this._transactionFromLine = transactionFromLine;
            this._whatNext = whatNext;
        }

        public bool Applies(string line)
        {
            bool success;
            (success, _transaction) = _transactionFromLine.GetFromLine(line);
            return success;
        }

        public void Process()
        {
            _whatNext(_transaction);
        }
    }
}