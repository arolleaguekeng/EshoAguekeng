using EshoAguekeng.Services;
using EshopAguekeng.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
//SecureStorage.RemoveAll();
namespace EshoAguekeng.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    
    public partial class MainPage : ContentPage
    {
        private readonly UserModel user;
        private bool reload = true;
        private bool selectedChange = true;


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

                //code secondaire a remplacer
                //var product = new ProductModel(1, "aa", "pharaon", "je suis une description", 5000f, null, 1, 1024, DateTime.Now);
                //var products = new List<ProductModel>();
                //products.Add(product);
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
            reload = true;
            await Navigation.PushModalAsync(new ProductEditPage(user,OnAppearing), true);
        }
        

        private void Rv_Refreshing(object sender, EventArgs e)
        {
            reload = true;
            Rv.IsRefreshing = false;
            OnAppearing();
        }

        private async void CvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(selectedChange)
            {
                var product = CvProduct.SelectedItem as ProductModel;
                
                
                if (product != null)
                {
                    reload = false;
                    await Navigation.PushAsync(new ProductDetailPage(user, OnAppearing, product), true);
                }
            }
            
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            var product = ((sender as StackLayout)?.GestureRecognizers[0] as TapGestureRecognizer)?.CommandParameter as ProductModel;
            if (product != null)
            {
                reload = false;
                await Navigation.PushAsync(new ProductBuyPage(user, OnAppearing, product), true);
            }
        }

        private async void btnProfile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProfilePage(user));
        }
    }
}
