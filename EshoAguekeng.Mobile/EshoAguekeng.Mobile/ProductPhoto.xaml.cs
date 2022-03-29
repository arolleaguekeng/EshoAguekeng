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
    public partial class ProductPhoto : ContentPage
    {
        private readonly ProductModel product;
        private readonly UserModel user;

        public ProductPhoto(UserModel user,ProductModel product)
        {
            this.product = product;
            this.user = user;
            InitializeComponent();
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {

        }
    }
}