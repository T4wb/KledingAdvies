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
    [Activity(Label = "GebruikerStylistUI")]
    public class GebruikerStylistUI : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GebruikerStylist);

            // init
            Button btnKoppelenKledingadvies = FindViewById<Button>(Resource.Id.btnKoppelenKledingadvies);
            Button btnTerugNaarMain = FindViewById<Button>(Resource.Id.btnTerugNaarMain);
            
            btnKoppelenKledingadvies.Click += delegate {
                StartActivity(typeof(KoppelenProductUI));
            };

            btnTerugNaarMain.Click += delegate {
                this.Finish();
            };
        }
    }
}