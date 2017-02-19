using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using System;

using System.Threading;
using System.Text.RegularExpressions;

using CC;
namespace UI
{
    [Activity(Label = "DialogSignUpActivity")]
    public class SignUpUserUI : Activity
    {
        // init managePersons object
        private GebruikersBeheer gebruikersbeheer = new GebruikersBeheer();

        // rol gebruiker voor geselecteerde spinneritem
        private string rolGebruiker;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Sets our view from the "SignUp" layout resource
            SetContentView(Resource.Layout.SignUp);

            // init
            TextView txtEmailRegister = FindViewById<TextView>(Resource.Id.txtEmailRegister);
            TextView txtPasswordRegister = FindViewById<TextView>(Resource.Id.txtPasswordRegister);

            Button btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            Button btnCancelRegister = FindViewById<Button>(Resource.Id.btnCancelRegister);

            // spinner
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner2);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.rolArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            btnSignUp.Click += delegate {

                // hides keyboard
                InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                if (string.IsNullOrEmpty(txtEmailRegister.Text))
                    LoginSignUpUtils.ShowTextviewError(txtEmailRegister, "Dit veld is leeg");
                else if (string.IsNullOrEmpty(txtPasswordRegister.Text))
                    LoginSignUpUtils.ShowTextviewError(txtPasswordRegister, "Dit veld is leeg");
                else
                {
                    bool validEmail = LoginSignUpUtils.IsValidEmail(txtEmailRegister.Text);
                    bool validPassword = LoginSignUpUtils.isValidPassword(txtPasswordRegister.Text);

                    if (validEmail && validPassword)
                        signUpRequest(txtEmailRegister, txtPasswordRegister);
                    else if (validEmail && !validPassword)
                        LoginSignUpUtils.ShowTextviewError(txtPasswordRegister, "Het wachtwoord moet minimaal uit 12 cijfers bestaan");
                    else
                        LoginSignUpUtils.ShowTextviewError(txtEmailRegister, "Dit is geen geldig e-mailadres");
                }
            };

            btnCancelRegister.Click += delegate
            {
                this.Finish();
            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // rolGebruiker neemt de waarde van het geselecteerde vak aan
            Spinner spinner = (Spinner)sender;
            rolGebruiker = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void signUpRequest(TextView txtEmailRegister, TextView txtPasswordRegister)
        {
            // shows progressdialog
            ProgressDialog progressDialog = ProgressDialog.Show(this, "", "Loading...", true, false);

            // simulates loading time
            new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(3000);
                this.RunOnUiThread(() =>
                {
                    // creates user if user doesn't exist in database and returns Boolean value userCreated
                    bool userCreated = checkSignUp(txtEmailRegister.Text, txtPasswordRegister.Text, rolGebruiker);

                    progressDialog.Dismiss();

                    if (userCreated)
                        this.Finish();
                });
            })).Start();
        }

        private bool checkSignUp(string txtEmailRegister, string txtPasswordRegister, string rolGebruiker)
        {
            // creates user if user doesn't exist in database
            bool userCreated = gebruikersbeheer.RegistreerGebruiker(txtEmailRegister, txtPasswordRegister, rolGebruiker);

            if (!userCreated)
                Toast.MakeText(this.BaseContext, "Deze gebruiker bestaat al", ToastLength.Short).Show();
            else
                Toast.MakeText(this.BaseContext, "De gebruiker is succesvol aangemaakt", ToastLength.Short).Show();

            return userCreated;
        }
    }
}

