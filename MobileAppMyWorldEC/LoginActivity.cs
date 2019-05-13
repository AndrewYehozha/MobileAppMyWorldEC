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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            Button btnLogIn = FindViewById<Button>(Resource.Id.button_login);
            btnLogIn.Click += delegate
            {
                LogIn();
            };

            Button btnReg = FindViewById<Button>(Resource.Id.button_registred);
            btnReg.Click += delegate
            {
                RegistredLoyout();
            };
        }

        private async void LogIn()
        {
            string emailField = FindViewById<EditText>(Resource.Id.EmailTextLogIn).Text;
            string passwordField = FindViewById<EditText>(Resource.Id.PasswordTextLogIn).Text;

            if (emailField == "" || passwordField == "")
            {
                Toast.MakeText(ApplicationContext, Resources.GetString(Resource.String.title_errorFields), ToastLength.Long).Show();
            }
            else
            {
                var model = new AuthorizationRequest
                {
                    Email = emailField,
                    Password = passwordField
                };

                bool IsAuth = await Authorization(model);

                if (IsAuth)
                {
                    Intent nextActivity = new Intent(this, typeof(MainActivity));
                    StartActivity(nextActivity);
                }
            }
        }

        private async Task<bool> Authorization(AuthorizationRequest model)
        {
            try
            {
                var responseString = await "http://localhost:60436/api/Auth/Authorization/".PostUrlEncodedAsync(model).ReceiveString();

                var success = JsonConvert.DeserializeObject<AuthorizationResponse>(responseString);
                if (success.Success)
                {
                    Data.Token = success.data.Token;
                    Data.UserId = success.data.UserId;

                    return true;
                }

                var error = JsonConvert.DeserializeObject<ErrorMessage>(responseString);

                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("Warning " + error.ErrorNum);
                alert.SetMessage(error.ErrorMessages);
                alert.SetNeutralButton("OK", delegate
                {
                    alert.Dispose();
                });
                alert.Show();

                return false;
            }
            catch { return false; }
        }

        private void RegistredLoyout()
        {
            Intent nextActivity = new Intent(this, typeof(RegisterActivity));
            StartActivity(nextActivity);
        }
    }
}