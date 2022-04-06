using EshoAguekeng.Services;
using EshopAguekeng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EshoAguekeng.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public  UserModel User { get; }
        private readonly Action callBack;

        public ProductModel Product { get; }

        public ProductDetailPage(UserModel user, Action callBack, ProductModel product)
        {
            InitializeComponent();
            this.User = user;
            this.callBack = callBack;
            Product = product;
            BindingContext = this;
        }



        private async void btnBuy_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            btnBuy.IsEnabled = false;
            try
            {
                await Navigation.PushModalAsync(new ProductBuyPage(User,callBack, Product));
            }
            catch (InvalidOperationException ex)
            {
                await DisplayAlert("Error", ex.Message +ex.InnerException, "Retry");
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", ex.Message, "Ok");
            }
            
            Loader.IsVisible = false;
            btnBuy.IsEnabled = true;
        }


        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        protected override void OnDisappearing()
        {
            if (callBack != null)
                callBack();
            base.OnDisappearing();
        }
    }
}