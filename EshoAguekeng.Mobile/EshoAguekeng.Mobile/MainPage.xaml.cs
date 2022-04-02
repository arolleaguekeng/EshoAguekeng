using EshoAguekeng.Services;
using EshopAguekeng.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EshoAguekeng.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly UserModel user;

        public MainPage(UserModel user)
        {
            InitializeComponent();
            this.user = user;
        }
        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                ProductService service = new ProductService(App.ServiceBaseAddress);
                var products = await service.GetAsync();
                CvProduct.ItemsSource = products;
                Console.WriteLine("------------------------------>"+products.First().Photo);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", ex.Message, "Ok");
            }
            base.OnAppearing();
            Loader.IsVisible = false;
            base.OnAppearing();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProductEdit(user,OnAppearing), true);
        }
        

        private void Rv_Refreshing(object sender, EventArgs e)
        {
            Rv.IsRefreshing = false;
            OnAppearing();
        }
    }
}
