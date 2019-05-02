using System;
using System.IO;
using iTextSharp.text.pdf;

namespace PDFReplace
{
    class Program
    {
        static void Main(string[] args)
        {
            string OrigFile = @"C:\Users\Zilioli\Downloads\opa.pdf";
            string ResultFile = @"C:\Users\Zilioli\Downloads\opa_novo.pdf";
            using (PdfReader reader = new PdfReader(OrigFile))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    byte[] contentBytes = reader.GetPageContent(i);
                    string contentString = PdfEncodings.ConvertToString(contentBytes, PdfObject.TEXT_PDFDOCENCODING);
                    contentString = contentString.Replace("#NOME#", "Carlos Zilioli");
                    contentString = contentString.Replace("#DATA#", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    reader.SetPageContent(i, PdfEncodings.ConvertToBytes(contentString, PdfObject.TEXT_PDFDOCENCODING));

                }
                new PdfStamper(reader, new FileStream(ResultFile, FileMode.Create, FileAccess.Write)).Close();

            }
        }
    }
}
