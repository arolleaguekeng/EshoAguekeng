using EshoAguekeng.Services;
using EshopAguekeng.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EshoAguekeng.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        //SecureStockage.RemoveAll
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnEye_Clicked(object sender, EventArgs e)
        {
            txtPassword.IsPassword = !txtPassword.IsPassword;
        }

        private async void btnConnect_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            btnConnect.IsEnabled = false;
            try
            {
                //UserService service = new UserService(App.ServiceBaseAddress);
                //UserModel user = await service.LoginAsync(txtUserName.Text, txtPassword.Text);
                var user = new UserModel(1024, "admin", "admin", "admin");
                await SecureStorage.SetAsync("user_session", JsonConvert.SerializeObject(user));
                Application.Current.MainPage = new NavigationPage(new MainPage(user));
                await DisplayAlert("Good", user?.Fullname, "Ok");
            }
            catch (UnauthorizedAccessException ex)
            {
                await DisplayAlert("Bad", ex.Message, "Ok");

            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", ex.Message, "Ok");
            }
            Loader.IsVisible = false;
            btnConnect.IsEnabled = true;
        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new FrmCreate());
        }
    }
}
