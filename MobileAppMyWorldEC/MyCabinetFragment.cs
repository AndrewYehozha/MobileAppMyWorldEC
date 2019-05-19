using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Flurl.Http;
using Newtonsoft.Json;
using MobileAppMyWorldEC.Models.Response;

namespace MobileAppMyWorldEC
{
    [Activity(Label = "@string/title_my_cabinet", Theme = "@style/AppTheme")]
    public class MyCabinetFragment : Android.Support.V4.App.Fragment
    {
        private View view;
        private UserViewModel userSuccsess;

        private EditText nameTextCabinet;
        private EditText surnameTextCabinet;
        private EditText countryTextCabinet;
        private EditText cityTextCabinet;
        private EditText addressTextCabinet;
        private EditText phoneTextCabinet;
        private EditText emailTextCabinet;
        private EditText birthdayTextCabinet;
        private TextView bonusScoreCabinet;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.fragment_MyCabinet, container, false);

            Button btnLogIn = view.FindViewById<Button>(Resource.Id.SaveInfoUser);
            btnLogIn.Click += delegate
            {
                SaveChanges();
            };

            nameTextCabinet = view.FindViewById<EditText>(Resource.Id.NameTextCabinet);
            surnameTextCabinet = view.FindViewById<EditText>(Resource.Id.SurnameTextCabinet);
            countryTextCabinet = view.FindViewById<EditText>(Resource.Id.CountryTextCabinet);
            cityTextCabinet = view.FindViewById<EditText>(Resource.Id.CityTextCabinet);
            addressTextCabinet = view.FindViewById<EditText>(Resource.Id.AddressTextCabinet);
            phoneTextCabinet = view.FindViewById<EditText>(Resource.Id.PhoneTextCabinet);
            emailTextCabinet = view.FindViewById<EditText>(Resource.Id.EmailTextCabinet);
            birthdayTextCabinet = view.FindViewById<EditText>(Resource.Id.BirthdayTextCabinet);
            bonusScoreCabinet = view.FindViewById<TextView>(Resource.Id.BonusScoreCabinet);
            LoadInfoUser();

            return view;
        }
        private async void SaveChanges()
        {
            if (nameTextCabinet.Text == "" || surnameTextCabinet.Text == "")
            {
                Toast.MakeText(this.Context, cityTextCabinet.Text, ToastLength.Long).Show();
            }
            //else
            //{
            //    var model = new AuthorizationRequest
            //    {
            //        Email = emailField,
            //        Password = passwordField
            //    };

            //    bool IsAuth = await Authorization(model);

            //    if (IsAuth)
            //    {
            //        Intent nextActivity = new Intent(this, typeof(MainActivity));
            //        StartActivity(nextActivity);
            //    }
            //}
        }

        //private async Task<bool> LoadInfoUser(AuthorizationRequest model)
        //{
        //    try
        //    {
        //        var responseString = await (Data.URL + "Auth/Authorization/").PostUrlEncodedAsync(model).ReceiveString();

        //        var success = JsonConvert.DeserializeObject<AuthorizationResponse>(responseString);
        //        if (success.Success)
        //        {
        //            Data.Token = success.data.Token;
        //            Data.UserId = success.data.UserId;

        //            return true;
        //        }

        //        var error = JsonConvert.DeserializeObject<ErrorMessage>(responseString);

        //        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
        //        alert.SetTitle("Warning " + error.ErrorNum);
        //        alert.SetMessage(error.ErrorMessages);
        //        alert.SetNeutralButton("OK", delegate
        //        {
        //            alert.Dispose();
        //        });
        //        alert.Show();

        //        return false;
        //    }
        //    catch { return false; }
        //}

        private async Task LoadInfoUser()
        {
            try
            {
                var responseString = await (Data.URL + "Users/GetUser/" + Data.UserId).WithHeader("Authorization", Data.Token).GetStringAsync();
                userSuccsess = JsonConvert.DeserializeObject<UserViewModel>(responseString);

                if (!userSuccsess.Success)
                {
                    var error = JsonConvert.DeserializeObject<ErrorMessage>(responseString);

                    Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(Context);
                    alert.SetTitle("Warning " + error.ErrorNum);
                    alert.SetMessage(error.ErrorMessages);
                    alert.SetNeutralButton("OK", delegate
                    {
                        alert.Dispose();
                    });
                    alert.Show();

                    return;
                }

                await LoadUserField(userSuccsess);
            }
            catch { }
        }

        private async Task LoadUserField(UserViewModel model)
        {
            nameTextCabinet.Text = model.data.FirstName;
            surnameTextCabinet.Text = model.data.LastName;
            countryTextCabinet.Text = model.data.Country;
            cityTextCabinet.Text = model.data.City;
            addressTextCabinet.Text = model.data.Address;
            phoneTextCabinet.Text = model.data.Phone;
            emailTextCabinet.Text = model.data.Email;
            birthdayTextCabinet.Text = model.data.Birsday.Value.ToString("MM.dd.yyyy");
            bonusScoreCabinet.Text = model.data.BonusScore.ToString();
        }
    }
}