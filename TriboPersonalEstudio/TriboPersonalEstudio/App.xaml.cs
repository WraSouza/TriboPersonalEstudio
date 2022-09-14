using OneSignalSDK.Xamarin;
using Plugin.FirebasePushNotification;
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

            OneSignal.Default.Initialize("34ed7075-36eb-4a64-bf85-e5b20eb19e85");
            OneSignal.Default.PromptForPushNotificationsWithUserResponse();


            //CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
        }

        private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Token: {e.Token}");
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
