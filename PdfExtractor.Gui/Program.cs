using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfExtractor.Gui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //string text = BetterPdfExtractor.pdfText(
            //    @"C:\Users\Luxor\Google Drive\Data\Finance\BBVA\Visa\2018\2018_07_26.pdf");
            //File.WriteAllText(@"C:\Users\Luxor\Google Drive\Data\Finance\BBVA\Visa\2018\2018_07_26.pdf.txt", text);
            //return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
