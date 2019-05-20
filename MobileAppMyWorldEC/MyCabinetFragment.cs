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
using MobileAppMyWorldEC.Models.Request;

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

            Button btnSave = view.FindViewById<Button>(Resource.Id.SaveInfoUser);
            btnSave.Click += delegate
            {
                SaveChanges();
            };

            Button btnChildrens = view.FindViewById<Button>(Resource.Id.OpenChildrens);
            btnChildrens.Click += delegate
            {

            };

            Button btnDiscountCards = view.FindViewById<Button>(Resource.Id.OpenDiscountCards);
            btnDiscountCards.Click += delegate
            {

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
            try
            {
                var model = new UserEditRequest
                {
                    Id = Data.UserId,
                    FirstName = nameTextCabinet.Text,
                    LastName = surnameTextCabinet.Text,
                    Country = countryTextCabinet.Text,
                    City = cityTextCabinet.Text,
                    Address = addressTextCabinet.Text,
                    Phone = phoneTextCabinet.Text,
                    Email = emailTextCabinet.Text,
                    Birsday = birthdayTextCabinet.Text
                };

                SendChanges(model);
            }
            catch { }
        }

        private async Task SendChanges(UserEditRequest model)
        {
            try
            {
                var responseString = await ((Data.URL + "Users/EditUser/").WithHeader("Authorization", Data.Token).PostUrlEncodedAsync(model)).ReceiveString();

                var success = JsonConvert.DeserializeObject<AuthorizationResponse>(responseString);
                if (!success.Success)
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
                }

                Toast.MakeText(Context, Resources.GetString(Resource.String.title_saveData), ToastLength.Long).Show();
            }
            catch { }
        }

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