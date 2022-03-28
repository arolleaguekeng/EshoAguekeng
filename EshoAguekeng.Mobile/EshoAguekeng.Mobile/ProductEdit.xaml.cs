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
    public partial class ProductEdit : ContentPage
    {
        public ProductEdit()
        {
            InitializeComponent();
        }

        private void btnNext_Clicked(object sender, EventArgs e)
        {
            framestepA.IsVisible = false;
            framestepB.IsVisible = true;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            btnSave.IsEnabled = false;
            try
            {
                UserService service = new UserService(App.ServiceBaseAddress);
                UserModel model =
                    new UserModel
                    (
                        0,
                        txtUserName.Text,
                        txtFullname.Text,
                        txtRole.Text,
                        txtPassword.Text
                    );
                var user = await service.CreateAsync(model);
                await DisplayAlert("User Added succesfully !", user.Fullname, "Continue");
            }
            catch (UnauthorizedAccessException ex)
            {
                await DisplayAlert("Error", ex.Message, "Retry");
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", "an error occured ! ", "Ok");
            }
            Loader.IsVisible = false;
            btnSave.IsEnabled = true;
        }

        private void btnPreview_Clicked(object sender, EventArgs e)
        {
            framestepA.IsVisible = true;
            framestepB.IsVisible = false;
        }
    }
}