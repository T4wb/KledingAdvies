using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

using Android.Views.InputMethods;
using System.Threading;

using CC;

namespace UI
{
    [Activity(Label = "LoginActivity")]
    public class LoginUserUI : Activity
    {
        private GebruikersBeheer gebruikersBeheer = new GebruikersBeheer();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Sets our view from the "Main" layout resource
            SetContentView(Resource.Layout.LoginIn);

            // init
            TextView txtEmailLogin = FindViewById<TextView>(Resource.Id.txtEmailLogin);
            TextView txtPasswordLogin = FindViewById<TextView>(Resource.Id.txtPasswordLogin);

            Button btnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            Button btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            Button btnTerugNaarMain = FindViewById<Button>(Resource.Id.btnTerugNaarMain);

            btnSignIn.Click += delegate {

                // hides the keyboard
                InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);


                if (string.IsNullOrEmpty(txtEmailLogin.Text))
                    LoginSignUpUtils.ShowTextviewError(txtEmailLogin, "Dit veld is leeg");
                else if (string.IsNullOrEmpty(txtPasswordLogin.Text))
                    LoginSignUpUtils.ShowTextviewError(txtPasswordLogin, "Dit veld is leeg");
                else
                {
                    bool validEmail = LoginSignUpUtils.IsValidEmail(txtEmailLogin.Text);

                    if (validEmail)
                        loginRequest(txtEmailLogin, txtPasswordLogin);
                    else
                        LoginSignUpUtils.ShowTextviewError(txtEmailLogin, "Dit is geen geldig e-mailadres");
                }
            };

            btnSignUp.Click += delegate {
                StartActivity(typeof(SignUpUserUI));
            };

            btnTerugNaarMain.Click += delegate {
                this.Finish();
            };
        }

        private void loginRequest(TextView txtEmailLogin, TextView txtPasswordLogin)
        {
            // shows progressdialog
            ProgressDialog progressDialog = ProgressDialog.Show(this, "", "Loading...", true, false);

            // simulates loading time
            new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(2500);
                this.RunOnUiThread(() =>
                {
                    // checks email in database, validates password and starts activity corresponding to the 'Role' of user
                    checkLogin(txtEmailLogin.Text, txtPasswordLogin.Text);
                    progressDialog.Dismiss();
                });
            })).Start();
        }

        private void checkLogin(string txtEmailLogin, string txtPasswordLogin)
        {
            string userState = gebruikersBeheer.CheckVerificatie(txtEmailLogin, txtPasswordLogin);

            if (userState == "Deze gebruiker bestaat niet" || userState == "Het ingevoerde wachtwoord is incorrect")
                Toast.MakeText(this.BaseContext, userState, ToastLength.Short).Show();
            else {
                // To Do: instead of toast start the activity associated the userState/Role
                //this.finish(); 
                //StartActivity(typeof(DialogSignUpActivity));
                findActivity(userState);
                Toast.MakeText(this.BaseContext, "Welkom " + userState, ToastLength.Short).Show();

            }
        }

        private void findActivity(string userState)
        {
            if (userState == "Klant")
                StartActivity(typeof(GebruikerKlantUI));
            else if (userState == "Stylist")
                StartActivity(typeof(GebruikerStylistUI));
            else if (userState == "Verkoper")
                StartActivity(typeof(GebruikerVerkoperUI));
            else if (userState == "Ondernemer")
                StartActivity(typeof(GebruikerOndernemerUI));

            Toast.MakeText(this.BaseContext, "Welkom " + userState, ToastLength.Short).Show();
            this.Finish();
        }
    }
}


