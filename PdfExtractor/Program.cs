using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.xmp;
using PdfExtractor.parsers;

namespace PdfExtractor
{
    class Program
    {
        static void Main(string[] args)
        {

            //string path = @"C:\Users\Luxor\Documents\Visual Studio 2017\Projects\PdfExtractor\TestFiles\mastercard";

            //foreach (string file in Directory.EnumerateFiles(path))
            //{
            //    var p = Path.GetDirectoryName(file);
            //    var f = Path.GetFileName(file);
            //    var e = Path.GetExtension(file);
            //    var fnoe = Path.GetFileNameWithoutExtension(file);

            //    string ttt = BetterPdfExtractor.pdfText(file);
            //    File.WriteAllText(Path.Combine(p, f+".txt"), ttt);
            //}

            //return;

            //string text = BetterPdfExtractor.pdfText(
            //    @"C:\Users\Luxor\Documents\Visual Studio 2017\Projects\PdfExtractor\TestFiles\2017_06_29.pdf");
            ////File.WriteAllText(@"C:\Users\Luxor\Documents\Visual Studio 2017\Projects\PdfExtractor\TestFiles\outText_2.txt", text);

            //Console.WriteLine(text);

            //Console.ReadLine();


        }
    }
}