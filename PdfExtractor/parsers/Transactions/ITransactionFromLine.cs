namespace PdfExtractor.parsers.Transactions
{
    public interface ITransactionFromLine
    {
        (bool Success, Transaction transaction) GetFromLine(string line);
    }
}