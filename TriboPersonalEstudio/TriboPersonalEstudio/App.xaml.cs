using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriboPersonalEstudio
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "Shell_UWP_Experimental" });
            InitializeComponent();

            MainPage = new View.LoginView();
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
