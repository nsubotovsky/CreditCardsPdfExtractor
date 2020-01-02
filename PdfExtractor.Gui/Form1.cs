using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfExtractor.parsers;

namespace PdfExtractor.Gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            string pdfAsText = BetterPdfExtractor.pdfText(file);

            IEnumerator<string> textEnumerator = ((IEnumerable<string>)pdfAsText.Split(Environment.NewLine.ToCharArray())).GetEnumerator();

            var parser = new ParserSelector().GetParser(textEnumerator);

            var b = parser.Parse(textEnumerator);

            //a.Parse(ttt);

            this.textBox1.Text = b.ToString();
        }
    }
}
