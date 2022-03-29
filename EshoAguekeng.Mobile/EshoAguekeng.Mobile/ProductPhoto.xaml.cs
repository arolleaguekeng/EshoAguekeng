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
        public ProductPhoto(ProductModel product)
        {
            this.product = product;
            InitializeComponent();
        }

    }
}