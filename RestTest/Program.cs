using System;

namespace RestTest
{
    public class Program
    {
        private const string URL = "https://ionlineapi.azurewebsites.net/";
        private const string REQUEST = "api/Acesso?IdUsuario={0}&IdEvento={1}";

        static void Main(string[] args)
        {
            Console.WriteLine(ProcessarEntrada(new ValidaAcessoRequest()
            {
                IdUsuario = "d3dcc6e3-70c2-4588-a9e1-a32afce95927",
                IdEvento = "36fab098-6de4-42e4-880d-b00946d44d14"
            }));

            Console.ReadKey();
        }

        static bool ProcessarEntrada(ValidaAcessoRequest objetoLeitura)
        {
            string uri = string.Format(REQUEST, objetoLeitura.IdUsuario, objetoLeitura.IdEvento);
            var client = new RestClient(URL);
            var request = new RestRequest(uri, Method.GET);

            var response = client.Execute(request);
            return true;
        }
    }

    public class ValidaAcessoRequest
    {
        public string IdUsuario { get; set; }
        public string IdEvento { get; set; }
    }
}
