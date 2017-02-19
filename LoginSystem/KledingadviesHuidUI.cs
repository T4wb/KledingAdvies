
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

using CC;

namespace UI
{
    [Activity(Label = "KledingadviesHuidActivity")]
    public class KledingadviesHuidUI : Activity
    {
        // init KledingadviesBeheer object
        private KledingadviesBeheer kledingadviesBeheer = new KledingadviesBeheer();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.KledingadviesHuid);

            // vangt kledingadvieskeuze array op
            string[] kledingadviesKeuze = Intent.GetStringArrayExtra("kledingadviesKeuze");

            // init
            ImageButton btn_white_skin = FindViewById<ImageButton>(Resource.Id.btn_white_skin);
            ImageButton btn_brown_skin = FindViewById<ImageButton>(Resource.Id.btn_brown_skin);
            ImageButton btn_black_skin = FindViewById<ImageButton>(Resource.Id.btn_black_skin);

            // Create your application here
            btn_white_skin.Click += delegate {
                kledingadviesKeuze[2] = "white";
                Btn_click(kledingadviesKeuze);
            };
            btn_brown_skin.Click += delegate {
                kledingadviesKeuze[2] = "brown";
                Btn_click(kledingadviesKeuze);
            };
            btn_black_skin.Click += delegate {
                kledingadviesKeuze[2] = "black";
                Btn_click(kledingadviesKeuze);
            };
        }

        private void Btn_click(string[] kledingadviesKeuze)
        {
            var kledingadviesResultaatUI = new Intent(this, typeof(KledingadviesLichaamstypeUI));
            kledingadviesResultaatUI.PutExtra("kledingadviesKeuze", kledingadviesKeuze);
            StartActivity(kledingadviesResultaatUI);
            this.Finish();
        }
    }
}

