using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PdfExtractor.parsers
{
    public class ParserSelector
    {
        private readonly Dictionary<string, IParser> _typesOfParsers;

        public ParserSelector()
        {
            this._typesOfParsers = new Dictionary<string, IParser>()
            {
                { @"^VISA PLATINUM", new VisaParser()},
                { @"MASTERCARD PLATINUM", new MasterCardParser()},
                
            };
        }

        public IParser GetParser(IEnumerator<string> lineEnumerator)
        {

            while (lineEnumerator.MoveNext())
            {
                foreach (KeyValuePair<string, IParser> regexParser in this._typesOfParsers)
                {
                    if (Regex.IsMatch(lineEnumerator.Current, regexParser.Key)) return regexParser.Value;
                }
            }

            return null;
        }


        

    }
}