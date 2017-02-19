using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Views.InputMethods;
using System.Threading;

using CC;


namespace UI
{
    [Activity(Label = "ToevoegenVerkoperUI")]
    public class ToevoegenVerkoperUI : Activity
    {
        private Verkopersbeheer verkopersBeheer = new Verkopersbeheer();
        private string emailOndernemer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Sets our view from the "Main" layout resource
            SetContentView(Resource.Layout.ToevoegenVerkoper);

            // init
            TextView txtEmailVerkoper = FindViewById<TextView>(Resource.Id.txtEmailVerkoper);
            Button btnOpslaanVerkoper = FindViewById<Button>(Resource.Id.btnOpslaanVerkoper);
            Button btnCancelToevoegenVerkoper = FindViewById<Button>(Resource.Id.btnCancelToevoegenVerkoper);

            // sets filiaalNaam
            // testing
            // To Do: set emailOndernemer to email ingelogde persoon! => creeer apart db TABLE LOGGED in: Kolom: email & Rol
            emailOndernemer = "testing";

            btnOpslaanVerkoper.Click += delegate
            {
                // hides the keyboard
                InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                if (string.IsNullOrEmpty(txtEmailVerkoper.Text))
                    LoginSignUpUtils.ShowTextviewError(txtEmailVerkoper, "Dit veld is leeg");
                else
                    toevoegRequest(txtEmailVerkoper);
            };

            btnCancelToevoegenVerkoper.Click += delegate
            {
                this.Finish();
            };
        }

        private void toevoegRequest(TextView txtEmailVerkoper)
        {
            // shows progressdialog
            ProgressDialog progressDialog = ProgressDialog.Show(this, "", "Het Systeem controleert of de verkoper bestaat...", true, false);

            // simulates loading time
            new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(2000);
                this.RunOnUiThread(() =>
                {
                    // checks email in database
                    verwerkCheckVerkoper(txtEmailVerkoper.Text);
                    progressDialog.Dismiss();
                });
            })).Start();
        }

        private void verwerkCheckVerkoper(string txtEmailVerkoper)
        {
            string userState = verkopersBeheer.CheckVerkoper(txtEmailVerkoper);

            if (userState == "Deze verkoper bestaat niet")
                Toast.MakeText(this.BaseContext, userState, ToastLength.Short).Show();
            else
            {
                verkopersBeheer.ToevoegenVerkoper(txtEmailVerkoper, emailOndernemer);
                this.Finish();
                Toast.MakeText(this.BaseContext, "Het toevoegen van " + txtEmailVerkoper + " is gelukt!", ToastLength.Short).Show();
            }
        }
    }
}