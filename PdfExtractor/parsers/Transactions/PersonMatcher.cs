using System.Globalization;
using System.Text.RegularExpressions;

namespace PdfExtractor.parsers.Transactions
{
    public class PersonMatcher : ITransactionFromLine
    {
        private readonly Regex _personIndicator;

        public PersonMatcher(string matchingRegex)
        {
            _personIndicator = new Regex(matchingRegex, RegexOptions.Compiled);
        }

        public (bool Success, Transaction transaction) GetFromLine(string line)
        {
            Match match = _personIndicator.Match(line);
            if (!match.Success) return (false, Transaction.EmtpyTransaction);

            string person = match.Groups["person"].Value.Trim();
            string capitalizedPerson = new CultureInfo("en-US", false).TextInfo.ToTitleCase(person.ToLower());

            return (true, new Transaction() {Who = capitalizedPerson});
        }
    }

}