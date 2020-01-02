using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PdfExtractor.parsers
{
    public abstract class Parser : IParser
    {
        protected IEnumerator<string> LineIterator = null;
        protected readonly TransactionList AllTransactions = new TransactionList();
        protected readonly List<Transaction> TempTransactions = new List<Transaction>();

        protected abstract List<TransactionMatcherAction> PossibleTransactions { get; }
        protected abstract string CloseDatePattern { get; }

        public virtual TransactionList Parse(IEnumerator<string> lineIterator)
        {
            LineIterator = lineIterator;
            AllTransactions.Cierre = FindClose();


            while (lineIterator.MoveNext())
            {
                PossibleTransactions.First(transactionMatcher => transactionMatcher.Applies(lineIterator.Current)).Process();
            }

            CommitRemainingTempTransactions();

            return AllTransactions;

        }

        public TransactionList Parse(IEnumerable<string> lines)
        {
            return this.Parse(lines.GetEnumerator());
        }

        protected void DoNothing(Transaction transaction)
        {

        }

        protected void AddToTempTransactions(Transaction transaction)
        {
            TempTransactions.Add(transaction);
        }
        protected void AddToAllTransactions(Transaction transaction)
        {
            AllTransactions.Add(transaction);
        }

        protected void CommitRemainingTempTransactions()
        {
            if (TempTransactions.Any())
            {
                TempTransactions.ForEach(t => t.Who = "OTROS");
                AllTransactions.AddRange(TempTransactions);
            }
        }

        protected virtual DateTime FindClose()
        {
            while (LineIterator.MoveNext())
            {
                Match match = Regex.Match(LineIterator.Current, CloseDatePattern);
                if (match.Success)
                {
                    return DateParsers.ParseRegexMatch(match);
                }
            }

            return default(DateTime);
        }

    }
}