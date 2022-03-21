using EshoAguekeng.Services;
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
        public MainPage()
        {
            InitializeComponent();
            txtUserName.Text = "Admin";
        }

        private async void btnConnect_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            btnConnect.IsEnabled = false;
            try
            {
                UserService service = new UserService(App.ServiceBaseAddress);
                var user = await service.LoginAsync(txtUserName.Text, txtPassword.Text);
                await DisplayAlert("Good", user.Fullname, "Ok");
            }
            catch(UnauthorizedAccessException ex)
            {
                await DisplayAlert("Bad", ex.Message, "Ok");
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", "an error occured ! ", "Ok");
            }
            Loader.IsVisible = false;
            btnConnect.IsEnabled = true;

        }
    }
}
