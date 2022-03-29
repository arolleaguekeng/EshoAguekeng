using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace EshoAguekeng.Mobile
{
    
    public partial class App : Application
    {
        private static string serviceBaseAddress;
        public static string ServiceBaseAddress => serviceBaseAddress;
        public App()
        {
            InitializeComponent();
            if (DeviceInfo.DeviceType == DeviceType.Virtual)
            {
                serviceBaseAddress = "http://192.168.137.1:8180/api";
            }
            else
            {
                serviceBaseAddress = "http://192.168.43.132:8180/api";
            }
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
