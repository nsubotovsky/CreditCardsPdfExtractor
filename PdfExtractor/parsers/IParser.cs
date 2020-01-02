using System.Collections.Generic;

namespace PdfExtractor.parsers
{
    public interface IParser
    {
        TransactionList Parse(IEnumerator<string> lines);

        TransactionList Parse(IEnumerable<string> lines);
    }
}