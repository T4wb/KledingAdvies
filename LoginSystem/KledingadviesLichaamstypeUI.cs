using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using CC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI
{
    [Activity(Label = "KledingadviesLichaamstypeUI")]
    public class KledingadviesLichaamstypeUI : Activity
    {
        // init KledingadviesBeheer object
        private KledingadviesBeheer kledingadviesBeheer = new KledingadviesBeheer();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.KledingadviesLichaamstype);

            // vangt kledingadvieskeuze array op
            string[] kledingadviesKeuze = Intent.GetStringArrayExtra("kledingadviesKeuze");

            //init 
            ImageButton btnEctomorph = FindViewById<ImageButton>(Resource.Id.btnEctomorph);
            ImageButton btnMesomorph = FindViewById<ImageButton>(Resource.Id.btnMesomorph);
            ImageButton btnEndomorph = FindViewById<ImageButton>(Resource.Id.btnEndomorph);

            btnEctomorph.Click += delegate
            {
                kledingadviesKeuze[3] = "Ectomorph";
                verwerkClick(kledingadviesKeuze);
            };

            btnMesomorph.Click += delegate
            {
                kledingadviesKeuze[3] = "Mesomorph";
                verwerkClick(kledingadviesKeuze);
            };

            btnEndomorph.Click += delegate
            {
                kledingadviesKeuze[3] = "Endomorph";
                verwerkClick(kledingadviesKeuze);
            };
       }

        private void verwerkClick(string[] kledingadviesKeuze)
        {
            // bepaalt seizoenstype, slaat kledingadvies op in db
            kledingadviesBeheer.insertKledingKeuze(kledingadviesKeuze);

            // toont succesbericht en eindigt activity
            Toast.MakeText(this.BaseContext, "Het kledingadvies is opgeslagen", ToastLength.Short).Show();
            this.Finish();
        }
    }
}