using Android.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: ExportFont("Samantha.ttf", Alias = "MyAwesomeCustomFont")]
[assembly: ExportFont("fa-brands-400.ttf", Alias = "brands")]
[assembly: ExportFont("fa-regular-400.ttf", Alias = "FontFaRegular400")]
[assembly: ExportFont("fa-solid-900.ttf", Alias = "FontFaSolid900")]
[assembly: ExportFont("fa-v4compatibility.ttf", Alias = "v4compatibility")]


[assembly: Application(UsesCleartextTraffic = true)]