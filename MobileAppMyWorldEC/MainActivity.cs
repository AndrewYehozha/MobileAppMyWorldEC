using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace MobileAppMyWorldEC
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            textMessage = FindViewById<TextView>(Resource.Id.message);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            //FindViewById<Button>(Resource.Id.navigation_preferences).Click += delegate
            //{
            //    Intent nextActivity = new Intent(this, typeof(LogInActivity));
            //    StartActivity(nextActivity);
            //};
            //FindViewById<Button>(Resource.Id.buttonSignupMain).Click += delegate
            //{
            //    Intent nextActivity = new Intent(this, typeof(SignUpActivity));
            //    StartActivity(nextActivity);
            //};
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_enterteinment_centers:
                    textMessage.SetText(Resource.String.title_enterteinment_centers);
                    return true;
                case Resource.Id.navigation_recommendations:
                    textMessage.SetText(Resource.String.title_recommendations);
                    return true;
                case Resource.Id.navigation_preferences:
                    textMessage.SetText(Resource.String.title_preferences);
                    return true;
                case Resource.Id.navigation_tickets:
                    textMessage.SetText(Resource.String.title_tickets);
                    return true;
                case Resource.Id.navigation_my_cabinet:
                    textMessage.SetText(Resource.String.title_my_cabinet);
                    return true;
            }
            return false;
        }
    }
}

