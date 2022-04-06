using EshopAguekeng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EshoAguekeng.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public UserModel User { get; }
        public ProfilePage(UserModel user)
        {
            InitializeComponent();
            this.User = user;
        }

        private async void btnLogOut_Clicked(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}