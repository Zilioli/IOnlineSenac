#region using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace UnimedFormPrinter.Model
{
    public class Input
    {
        public string Path { get; set; }
        public string FileName { get; set; }

        public string NumeroGuia { get; set; }
        public string CodigoOperadora { get; set; }
        public string NomeContratado { get; set; }
        public List<Procedimento> Procedimentos { get; set; }
        public decimal TotalMateria { get; set; }
        public decimal TotalDiaria { get; set; }
        public decimal TotalGeral { get; set; }

    }
}
