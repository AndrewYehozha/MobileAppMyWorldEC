using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Flurl.Http;
using MobileAppMyWorldEC.Models.Request;
using Newtonsoft.Json;
using MobileAppMyWorldEC.Models.Response;
using Android.Support.V7.App;

namespace MobileAppMyWorldEC
{
    [Activity(Label = "@string/title_mall_centers", Theme = "@style/AppTheme")]
    public class ECentersFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            
            return inflater.Inflate(Resource.Layout.fragment_ECenters, container, false);
        }
    }
}