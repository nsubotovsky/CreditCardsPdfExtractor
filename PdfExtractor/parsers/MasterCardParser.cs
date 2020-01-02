using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using PdfExtractor.parsers.Transactions;

namespace PdfExtractor.parsers
{
    public class MasterCardParser : Parser
    {
        public MasterCardParser()
        {
            PossibleTransactions = new List<TransactionMatcherAction>()
            {
                new TransactionMatcherAction(new MasterCardNormalTransaction(), AddToTempTransactions),
                new TransactionMatcherAction(new MasterCardPaymentTransaction(), AddToAllTransactions),
                new TransactionMatcherAction(new MasterCardSpecialTransaction(), AddToAllTransactions),
                new TransactionMatcherAction(new MasterCardIvaTransaction(), UpdateDateAndAddToAllTransactions),
                new TransactionMatcherAction(new PersonMatcher(@"TOTAL (TITULAR|ADICIONAL)\s+(?<person>\D+)"), UpdatePersonsForTempTransactionsAndCommit),
                new TransactionMatcherAction(new EmptyMatcher(), DoNothing),
            };
        }


        private void UpdateDateAndAddToAllTransactions(Transaction transaction)
        {
            transaction.Date = AllTransactions.Cierre;
            AllTransactions.Add(transaction);
        }

        private void UpdatePersonsForTempTransactionsAndCommit(Transaction transaction)
        {
            TempTransactions.ForEach(t => t.Who = transaction.Who);
            AllTransactions.AddRange(TempTransactions);
            TempTransactions.Clear();
        }


        protected override List<TransactionMatcherAction> PossibleTransactions { get; }



        protected override string CloseDatePattern => @"^Estado de cuenta al:\s+(?<day>\d+)-(?<month>\S+)-(?<year>\d+)\s.*$";

    }
}