using Foundation;
using UIKit;
using com.b_velop.OneMediPlan.iOS.Helper;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Domain.Stores;
using com.b_velop.OneMediPlan.Services;

namespace com.b_velop.OneMediPlan.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        public override UIWindow Window { get; set; }

        public static void SetNotification(Medi medi)
        {
            var newTime = medi.NextDate.DateTime.ToNSDate();

            var notification = new UILocalNotification();
            notification.FireDate = newTime;
            //notification.FireDate = NSDate.FromTimeIntervalSinceNow(15);
            notification.AlertTitle = $"{medi.Name}"; // required for Apple Watch notifications
            notification.AlertAction = $"View Alert for {medi.Name}";
            notification.AlertBody = $"Zeit für {medi.Dosage} Einheit(en)!";
            notification.SoundName = UILocalNotification.DefaultSoundName;

            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Localization:
            // https://docs.microsoft.com/de-de/xamarin/ios/app-fundamentals/localization/
            App.SetNotification = SetNotification;
            var notificationSettings =
                UIUserNotificationSettings
                    .GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);
            application.RegisterUserNotificationSettings(notificationSettings);
            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override async void DidEnterBackground(UIApplication application)
        {
            var st = new LocalDataStore();
            var inst = AppStore.Instance;
            if (inst.User == null) return;
            var user = inst.User;
            var ll = await st.PersistToDevice(user, "user.json");
            var llwd = await st.PersistToDevice(AppStore.Instance.Weekdays, "weekdays.json");
            var ass = await st.PersistToDevice(AppStore.Instance.AppSettings, "settings.json");
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}
