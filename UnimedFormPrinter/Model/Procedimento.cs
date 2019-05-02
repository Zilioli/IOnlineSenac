namespace UnimedFormPrinter.Model
{
    public class Procedimento
    {
        public int Tabela { get; set; }
        public string Registro { get; set; }
        public string Data { get; set; }
        public string CodigoItem { get; set; }
        public string DescricaoItem { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
