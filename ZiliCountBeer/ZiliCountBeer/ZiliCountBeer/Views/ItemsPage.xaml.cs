using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZiliCountBeer.Models;
using ZiliCountBeer.Views;
using ZiliCountBeer.ViewModels;

namespace ZiliCountBeer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }
      

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}