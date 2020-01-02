using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using PdfExtractor.parsers.Transactions;

namespace PdfExtractor.parsers
{
    public class VisaParser : Parser
    {
        public VisaParser()
        {
            PossibleTransactions = new List<TransactionMatcherAction>()
            {
                new TransactionMatcherAction(new VisaNormalTransaction(), AddToTempTransactions),
                new TransactionMatcherAction(new PersonMatcher(@"Total Consumos de(?<person>.{45})"), UpdatePersonsForTempTransactionsAndCommit),
                new TransactionMatcherAction(new EmptyMatcher(), DoNothing),
            };
        }
            
        protected override List<TransactionMatcherAction> PossibleTransactions { get; }
        protected override string CloseDatePattern => @"^CIERRE ACTUAL:\s(?<day>\d+)\s(?<month>\S+)\s(?<year>\d+)$";

        private void UpdatePersonsForTempTransactionsAndCommit(Transaction transaction)
        {
            TempTransactions.ForEach(t => t.Who = t.Description.StartsWith("SU PAGO EN ") ? "PAGO" : transaction.Who);
            AllTransactions.AddRange(TempTransactions);
            TempTransactions.Clear();
        }

        

        public static Transaction ExtractTransactionFromMatch(Match transactionMatch)
        {
            DateTime transactionDate = DateParsers.ExtractDateFromString(transactionMatch.Groups["date"].Value);

            string description = transactionMatch.Groups["description"].Value.Trim();
            string amountInArs = transactionMatch.Groups["amountInArs"].Value.Trim(' ', '_');
            string amountInUsd = transactionMatch.Groups["amountInUsd"].Value.Trim(' ', '_');
            var amountToParse = String.IsNullOrWhiteSpace(amountInUsd)
                ? new {ccy = "ars", amount = amountInArs}
                : new {ccy = "usd", amount = amountInUsd};

            return new Transaction()
            {
                Date = transactionDate,
                Amount = decimal.Parse(amountToParse.amount, new CultureInfo("es-AR")),
                Currency = amountToParse.ccy,
                Description = description,
                Who = ""
            };
        }







    }
}