using EshoAguekeng.Services;
using EshopAguekeng.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EshoAguekeng.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPhoto : ContentPage
    {
        private readonly UserModel user;
        private  ProductModel product;
        private string imageFile;

        public ProductPhoto(UserModel user, ProductModel product)
        {
            InitializeComponent();
            this.user = user;
            this.product = product;
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }


        private async void btnPicture_Clicked(object sender, EventArgs e)
        {
            string[] buttons =  { "Galery", "Camera" };
            var result = await DisplayActionSheet
                (
                    "Take a picture",
                    "Cancel",
                    string.Empty,
                    buttons                    
                );

            
            FileResult photo = null;
            if (result == buttons[0])
            {
                 photo = await MediaPicker.PickPhotoAsync
                    (
                        new MediaPickerOptions { Title = "Take a picture" }
                    );
            }

            else if (result == buttons[1])
            {
                 photo = await MediaPicker.CapturePhotoAsync
                    (
                        new MediaPickerOptions { Title = "Take a picture"}
                    );
                                   
            }
            Loader.IsVisible = true;
            imageFile = await loadPicture(photo);
            if (!string.IsNullOrEmpty(imageFile))
                Img.Source = ImageSource.FromFile(imageFile);
            Loader.IsVisible = false;
        }
        private async Task<string> loadPicture(FileResult photo)
        {
            if (photo == null)
                return null;
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            {
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }
            }
            return newFile;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            btnSave.IsEnabled = false;
            try
            {
                if (string.IsNullOrEmpty(imageFile))
                    throw new InvalidOperationException("Please take a picture");
                ProductService service = new ProductService(App.ServiceBaseAddress);
                product = await service.CreateAsync(product,File.ReadAllBytes(imageFile));

                await DisplayAlert("Good", "Save done", "Ok");
                await Navigation.PopToRootAsync(true);
            }
            catch (InvalidOperationException ex)
            {
                await DisplayAlert("Error", ex.Message, "Retry");
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", ex.Message, "Ok");
            }
            Loader.IsVisible = false;
            btnSave.IsEnabled = true;
        }
    }
}