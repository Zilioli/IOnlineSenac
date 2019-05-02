using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace IOnlineSenac
{
    public partial class MainPage : ContentPage
    {

        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        private const string URL = "https://ionlineapi.azurewebsites.net/";
        private const string REQUEST = "api/Acesso?IdUsuario={0}&IdEvento={1}";

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(zxing != null)
                zxing.IsScanning = true;
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void LerQRCode(object sender, EventArgs e)
        {
            IniciarQRCode();
        }

        private void IniciarQRCode()
        {

            //Opções de Leitura
            var options = new ZXing.Mobile.MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                UseFrontCameraIfAvailable = false,
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                {
                    ZXing.BarcodeFormat.QR_CODE //ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13
                }
            };

            zxing = new ZXing.Net.Mobile.Forms.ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Options = options,
                IsScanning = true
            };

            zxing.OnScanResult += (result) =>
                 Device.BeginInvokeOnMainThread(async () =>
                 {
                     // Para a analise
                     zxing.IsAnalyzing = false;

                     try
                     {
                         ValidaAcessoRequest resultado = JsonConvert.DeserializeObject<ValidaAcessoRequest>(result.Text);

                         if (ProcessarEntrada(resultado))
                             await DisplayAlert("Leitura realizada", "Entrada Confirmada", "OK");
                         else
                             await DisplayAlert("Leitura realizada", "Você já entrou", "OK");
                     }
                     catch 
                     {
                         await DisplayAlert("Leitura realizada", "QRCode não é válido", "OK");
                     }

                     zxing.IsAnalyzing = true;
                 });

            overlay = new ZXingDefaultOverlay
            {
                TopText = "Escolhe um QRCode para leitura",
                BottomText = "O Código sera lido automaticamente",
                ShowFlashButton = zxing.HasTorch, //Lanterna
            };

            overlay.FlashButtonClicked += (sender, e) =>
            {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };

            var abort = new Button
            {
                Text = "Cancelar",
                VerticalOptions = LayoutOptions.End,
                TextColor = Color.FromHex("#FFF"),
                BackgroundColor = Color.FromHex("#4F51FF"),
                HeightRequest = 40
            };

            abort.Clicked += (object s, EventArgs e) =>
            {
                zxing.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();
                });
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            grid.Children.Add(zxing);
            grid.Children.Add(overlay);
            //grid.Children.Add(abort);

            Content = grid;
        }

        protected override void OnDisappearing()
        {
            if(zxing != null)
                zxing.IsScanning = false;

            base.OnDisappearing();
        }

        private bool ProcessarEntrada(ValidaAcessoRequest objetoLeitura)
        {
            string uri = string.Format(REQUEST, objetoLeitura.IdUsuario, objetoLeitura.IdEvento);
            var client = new RestClient(URL);
            var request = new RestRequest(uri, Method.GET);
           
            var response = client.Execute<bool>(request).Data;
            return response;
        }
    }
}
