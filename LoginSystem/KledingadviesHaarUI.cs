
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
    [Activity(Label = "KledingadviesHaarActivity")]
    public class KledingadviesHaarUI : Activity
    {
        // init KledingadviesBeheer object
        private KledingadviesBeheer kledingadviesBeheer = new KledingadviesBeheer();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.KledingadviesHaar);

            // vangt kledingadvieskeuze array op
            string[] kledingadviesKeuze = Intent.GetStringArrayExtra("kledingadviesKeuze");

            // init
            TextView textView1 = FindViewById<TextView>(Resource.Id.textView1);

            ImageButton btn_light_blond_hair = FindViewById<ImageButton>(Resource.Id.btn_light_blond_hair);
            ImageButton btn_medium_blond_hair = FindViewById<ImageButton>(Resource.Id.btn_medium_blond_hair);
            ImageButton btn_dark_blond_hair = FindViewById<ImageButton>(Resource.Id.btn_dark_blond_hair);
            ImageButton btn_light_brown_hair = FindViewById<ImageButton>(Resource.Id.btn_light_brown_hair);
            ImageButton btn_medium_brown_hair = FindViewById<ImageButton>(Resource.Id.btn_medium_brown_hair);
            ImageButton btn_dark_brown_hair = FindViewById<ImageButton>(Resource.Id.btn_dark_brown_hair);
            ImageButton btn_light_grey_hair = FindViewById<ImageButton>(Resource.Id.btn_light_grey_hair);
            ImageButton btn_dark_grey_hair = FindViewById<ImageButton>(Resource.Id.btn_dark_grey_hair);
            ImageButton btn_black_hair = FindViewById<ImageButton>(Resource.Id.btn_black_hair);

            btn_light_blond_hair.Click += delegate {
                kledingadviesKeuze[1] = "light_blond";
                Btn_click(kledingadviesKeuze);
            };

            btn_medium_blond_hair.Click += delegate {
                kledingadviesKeuze[1] = "medium_blond";
                Btn_click(kledingadviesKeuze);
            };
            btn_dark_blond_hair.Click += delegate {
                kledingadviesKeuze[1] = "dark_blond";
                Btn_click(kledingadviesKeuze);
            };
            btn_light_brown_hair.Click += delegate {
                kledingadviesKeuze[1] = "light_brown";
                Btn_click(kledingadviesKeuze);
            };
            btn_medium_brown_hair.Click += delegate {
                kledingadviesKeuze[1] = "medium_brown";
                Btn_click(kledingadviesKeuze);
            };
            btn_dark_brown_hair.Click += delegate {
                kledingadviesKeuze[1] = "dark_brown";
                Btn_click(kledingadviesKeuze);
            };
            btn_light_grey_hair.Click += delegate {
                kledingadviesKeuze[1] = "light_grey";
                Btn_click(kledingadviesKeuze);
            };
            btn_dark_grey_hair.Click += delegate {
                kledingadviesKeuze[1] = "dark_grey";
                Btn_click(kledingadviesKeuze);
            };
            btn_black_hair.Click += delegate {
                kledingadviesKeuze[1] = "black";
                Btn_click(kledingadviesKeuze);
            };
        }
        private void Btn_click(string[] kledingadviesKeuze)
        {
            var kledingadviesHuidUI = new Intent(this, typeof(KledingadviesHuidUI));
            kledingadviesHuidUI.PutExtra("kledingadviesKeuze", kledingadviesKeuze);
            StartActivity(kledingadviesHuidUI);
            this.Finish();
        }
    }
}

