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
    [Activity(Label = "GebruikerKlantUI")]
    public class GebruikerKlantUI : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GebruikerKlant);

            //init
            Button btnVaststellenKledingadvies = FindViewById<Button>(Resource.Id.btnVaststellenKledingadvies);
            Button btnTonenKledingadvies = FindViewById<Button>(Resource.Id.btnTonenKledingadvies);
            Button btnTerugNaarMain = FindViewById<Button>(Resource.Id.btnTerugNaarMain);

            btnVaststellenKledingadvies.Click += delegate {
                // creates string array keuze with 5 elements
                string[] kledingadviesKeuze = new string[5];

                var kledingadviesOogUI = new Intent(this, typeof(KledingadviesOogUI));
                kledingadviesOogUI.PutExtra("kledingadviesKeuze", kledingadviesKeuze);
                StartActivity(kledingadviesOogUI);
            };

            btnTonenKledingadvies.Click += delegate {
                StartActivity(typeof(TonenKledingadviesUI));
            };

            btnTerugNaarMain.Click += delegate {
                this.Finish();
            };
        }
    }
}