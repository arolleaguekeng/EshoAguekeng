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
        private readonly UserModel user;
        private readonly Action callBack;

        public ProductEdit(UserModel user, Action callBack)
        {
            InitializeComponent();
            this.user = user;
            this.callBack = callBack;
        }
        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                //CategoryService service = new CategoryService(App.ServiceBaseAddress);
                //var  categories = await service.GetAsync();
                //var list = categories.ToList();
                var list = new List<CategoryModel>();

                //code temporaire
                //

                list.Insert(0, new CategoryModel { Name = "Choose a category" });
                list.Insert(1, new CategoryModel { Name = "Smartphone" });
                CbCategory.ItemsSource = list;
                CbCategory.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", ex.Message, "Ok");
            }
            base.OnAppearing();
            Loader.IsVisible = false;
        }

        private void btnNext_Clicked(object sender, EventArgs e)
        {
            framestepA.IsVisible = false;
            framestepB.IsVisible = true;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            CheckForm();
            Loader.IsVisible = true;
            btnSave.IsEnabled = false;
            try
            {
                var product = new ProductModel
                    (
                        0,
                        txtCode.Text,
                        txtName.Text,
                        txtdescription.Text,
                        float.Parse(txtPrice.Text),
                        string.Empty,
                        user.Id,
                        (CbCategory.SelectedItem as CategoryModel)?.Id ?? 0,
                        DateTime.Now
                      
                    );
                await Navigation.PushModalAsync(new ProductPhoto(user,product,callBack));
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
            btnSave.IsEnabled = true;
        }

        private void btnPreview_Clicked(object sender, EventArgs e)
        {
            framestepA.IsVisible = true;
            framestepB.IsVisible = false;
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private void CheckForm()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                throw new InvalidOperationException("please enter product code !");
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                throw new InvalidOperationException("please enter product Name !");
            }
            if (string.IsNullOrEmpty(txtdescription.Text))
            {
                throw new InvalidOperationException("please enter product Description !");
            }
            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                throw new InvalidOperationException("please enter product Price !");
            }
            if (CbCategory.SelectedIndex <= 0)
            {
                throw new InvalidOperationException("please choose product category !");
            }

        }

        protected override void OnDisappearing()
        {
            if (callBack != null)
                callBack();
            base.OnDisappearing();
        }
    }
}