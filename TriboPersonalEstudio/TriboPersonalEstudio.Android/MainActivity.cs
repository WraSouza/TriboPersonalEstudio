using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.LocalNotification;
using Plugin.FirebasePushNotification;
using OneSignalSDK.Xamarin;

namespace TriboPersonalEstudio.Droid
{
    [Activity( ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            OneSignal.Default.Initialize("34ed7075-36eb-4a64-bf85-e5b20eb19e85");
            OneSignal.Default.PromptForPushNotificationsWithUserResponse();
            //LocalNotificationCenter.CreateNotificationChannel();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}