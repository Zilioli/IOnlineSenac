#region using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace UnimedFormPrinter.Model
{
    public class OutPut
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public byte[] PDFFile { get; set; }
    }
}