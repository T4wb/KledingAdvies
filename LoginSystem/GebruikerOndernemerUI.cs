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

namespace UI
{
    [Activity(Label = "GebruikerOndernemerUI")]
    public class GebruikerOndernemerUI : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GebruikerOndernemer);

            // init
            Button btnToevoegenProduct = FindViewById<Button>(Resource.Id.btnToevoegenProduct);
            Button btnToevoegenVerkoper = FindViewById<Button>(Resource.Id.btnToevoegenVerkoper);
            Button btnTerugNaarMain = FindViewById<Button>(Resource.Id.btnTerugNaarMain);

            btnToevoegenProduct.Click += delegate {
                StartActivity(typeof(ToevoegenProductUI));
            };

            btnToevoegenVerkoper.Click += delegate {
                StartActivity(typeof(ToevoegenVerkoperUI));
            };

            btnTerugNaarMain.Click += delegate {
                this.Finish();
            };
        }
    }
}