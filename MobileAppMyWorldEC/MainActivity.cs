using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace MobileAppMyWorldEC
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private BottomNavigationView navigation;
        private Stack<SupportFragment> mStackFragment;
        private Stack<int> mStackSelectedNav;
        private SupportFragment mCurrentFragment;

        private ECentersFragment eCentersFragment;
        private RecommendationsFragment recommendationsFragment;
        private PreferencesFragment preferencesFragment;
        private TicketsFragment ticketsFragment;
        private MyCabinetFragment myCabinetFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.Title = Resources.GetString(Resource.String.title_mall_centers);
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            mStackFragment = new Stack<SupportFragment>();
            mStackSelectedNav = new Stack<int>();

            eCentersFragment = new ECentersFragment();
            recommendationsFragment = new RecommendationsFragment();
            preferencesFragment = new PreferencesFragment();
            ticketsFragment = new TicketsFragment();
            myCabinetFragment = new MyCabinetFragment();


            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, eCentersFragment, Resources.GetString(Resource.String.title_mall_centers));
            trans.Add(Resource.Id.fragmentContainer, recommendationsFragment, Resources.GetString(Resource.String.title_recommendations));
            trans.Add(Resource.Id.fragmentContainer, preferencesFragment, Resources.GetString(Resource.String.title_preferences));
            trans.Add(Resource.Id.fragmentContainer, ticketsFragment, Resources.GetString(Resource.String.title_tickets));
            trans.Add(Resource.Id.fragmentContainer, myCabinetFragment, Resources.GetString(Resource.String.title_my_cabinet));
            trans.Hide(recommendationsFragment);
            trans.Hide(preferencesFragment);
            trans.Hide(ticketsFragment);
            trans.Hide(myCabinetFragment);
            trans.Commit();

            mStackSelectedNav.Push(0);
            mCurrentFragment = eCentersFragment;
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
                    mStackSelectedNav.Push(0);
                    ShowFragment(eCentersFragment);
                    return true;
                case Resource.Id.navigation_recommendations:
                    mStackSelectedNav.Push(1);
                    ShowFragment(recommendationsFragment);
                    return true;
                case Resource.Id.navigation_preferences:
                    mStackSelectedNav.Push(2);
                    ShowFragment(preferencesFragment);
                    return true;
                case Resource.Id.navigation_tickets:
                    mStackSelectedNav.Push(3);
                    ShowFragment(ticketsFragment);
                    return true;
                case Resource.Id.navigation_my_cabinet:
                    mStackSelectedNav.Push(4);
                    ShowFragment(myCabinetFragment);
                    return true;
            }
            return false;
        }

        private void ShowFragment(SupportFragment fragment)
        {
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Hide(mCurrentFragment);
            trans.Show(fragment);
            trans.AddToBackStack(null);
            trans.Commit();

            this.Title = fragment.Tag;
            mStackFragment.Push(mCurrentFragment);
            mCurrentFragment = fragment;
        }

        public override void OnBackPressed()
        {
            if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                this.Title = mStackFragment.Peek().Tag;
                mStackSelectedNav.Pop();
                navigation.Menu.GetItem(mStackSelectedNav.Peek()).SetChecked(true);

                SupportFragmentManager.PopBackStack();
                mCurrentFragment = mStackFragment.Pop();
            }
            else
            {
                base.OnBackPressed();
            }
        }
    }
}

