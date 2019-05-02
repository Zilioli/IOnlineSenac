#region using
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Drawing;
using System.IO;
using UnimedFormPrinter.Model;
#endregion

namespace UnimedFormPrinter
{
    public class PDF
    {

        #region Constantes
        private const string CREATOR = "Zilioli Software";
        private const int FONT_SIZE = 12;
        private const string FONT_NAME = BaseFont.TIMES_ROMAN;
        private const int FONT_STYLE = Font.NORMAL;
        #endregion

        #region Properties
        private Document _docPDF = null;
        private Input _input;
        private BaseFont _fontBase;
        private Font _defautlFont;
        private string _path = "";
        #endregion

        #region Constructor
        public PDF(Input input)
        {
            _docPDF = new Document();
            _docPDF.SetPageSize(PageSize.A3.Rotate());
            _docPDF.SetMargins(0, 0, 0, 0);
            _docPDF.AddCreationDate();
            _input = input;
            _fontBase = BaseFont.CreateFont(FONT_NAME, BaseFont.CP1252, false);
            _defautlFont = new Font(_fontBase, FONT_SIZE, FONT_STYLE, BaseColor.BLACK);
            _path = $"{_input.Path}/{_input.FileName}";
        }
        #endregion

        #region Methods
        public OutPut Print()
        {
            OutPut _outPut = new OutPut();

            PdfWriter writer = PdfWriter.GetInstance(_docPDF, new FileStream(_path, FileMode.Create));

            _docPDF.Open();

            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(_fontBase, 12);

            for (int cont = 0; cont <= 10; cont++)
            {
                cb.SetTextMatrix(20, PageSize.A3.Rotate().Height - (20 * (cont - 1)));  //(xPos, yPos)
                cb.ShowText("Some text here and the Date: " + DateTime.Now.ToShortDateString());
            }


            cb.EndText();

            _docPDF.Close();

            return _outPut;
        }

        public OutPut PrintFromHtml()
        {
            string linha = $@"";

            foreach (Procedimento item in _input.Procedimentos)
                linha += $@"
                                <tr>
                                    <td>{item.Registro}</td>
                                    <td>{item.Data}</td>
                                    <td>{item.Tabela}</td>
                                    <td>{item.CodigoItem}</td>
                                    <td>{item.Quantidade}</td>
                                    <td>{item.Valor}</td>
                                </tr>
                                <tr>
                                    <td colspan='3'>&nbsp;</td>
                                    <td colspan='3'>{item.DescricaoItem}</td>
                                </tr>
                            ";

            string html = $@"
                            <!DOCTYPE html>
                            <html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
                            <head>
                                <meta charset='utf-8' />
                            </head>
                            <body>
                                <table style='border:1px solid;width:100%;height:100%;margin: 0,0,0,0;'>
                                    <tr style='height:4cm'><td colspan='6'></td></tr>
                                    <tr>
                                        <td>
                                            <table style='width:100%;height:1cm'>
                                                {linha}
                                            </table>
                                        </td>
                                    </tr>

                                </table>
                            </body>
                            </html>
                            ";

            using (var writer = PdfWriter.GetInstance(_docPDF, new FileStream(_path, FileMode.Create)))
            {
                _docPDF.Open();

                using (var srHtml = new StringReader(html))
                {
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, _docPDF, srHtml);
                }
                _docPDF.Close();
            }

            return null;
        }
        #endregion
    }
}