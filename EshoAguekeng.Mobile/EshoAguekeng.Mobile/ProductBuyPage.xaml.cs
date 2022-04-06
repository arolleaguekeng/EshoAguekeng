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
    public partial class ProductBuyPage : ContentPage
    {
        private readonly UserModel user;
        private readonly Action callBack;

        public ProductModel Product { get; }

        public ProductBuyPage(UserModel user, Action callBack, ProductModel product)
        {
            InitializeComponent();
            this.user = user;
            this.callBack = callBack;
            Product = product;
            BindingContext = this;
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private async void btnBuy_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;

            try
            {
                //await Navigation.PushModalAsync(new ProductPhoto(user,product,callBack));
                int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;

                if (callBack != null)
                    callBack();

                for (int currModal = 0; currModal < numModals; currModal++)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
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
        }


 

        protected override void OnDisappearing()
        {
            if (callBack != null)
                callBack();
            base.OnDisappearing();
        }
    }
}