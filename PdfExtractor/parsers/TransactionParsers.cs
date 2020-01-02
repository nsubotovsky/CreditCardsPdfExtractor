using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PdfExtractor.parsers
{
    internal static class TransactionParsers
    {
        private static readonly List<string> CurrenciesList = new List<string>() { "USD", "CLP", "UYU", "EUR" };

        public static Transaction ExtractTransactionFromMatch(Match transactionMatch)
        {
            DateTime transactionDate = DateParsers.ParseString(transactionMatch.Groups["date"].Value);

            string description = transactionMatch.Groups["description"].Value.Trim();
            string amount = transactionMatch.Groups["amount"].Value.Trim(' ', '_');
            string currency = DetectCurrencyFromDescription(description);

            return new Transaction()
            {
                Date = transactionDate,
                Amount = Decimal.Parse(amount, new CultureInfo("es-AR")),
                Currency = currency,
                Description = description,
                Who = ""
            };
        }

        private static string DetectCurrencyFromDescription(string description)
        {
            string regexPattern = ",(" + String.Join("|", CurrenciesList) + @").*\)$";
            return Regex.IsMatch(description, regexPattern) ? "usd" : "ars";
        }
    }
}