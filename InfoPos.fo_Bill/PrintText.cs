using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;

namespace PrintText
{
    public class PrintText
    {
        private Font _font = null;
        private string _text = null;

        public void Print(System.Drawing.Font font, string printername, string text)
        {
            try
            {
                using (PrintDocument doc = new PrintDocument())
                {
                    _font = font;
                    _text = text;

                    doc.PrinterSettings.PrinterName = printername;
                    doc.PrintPage += new PrintPageEventHandler(doc_PrintPage);
                    doc.Print();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int y = e.MarginBounds.Y;

            e.Graphics.DrawString(_text, _font, Brushes.Azure, 300, y);

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
