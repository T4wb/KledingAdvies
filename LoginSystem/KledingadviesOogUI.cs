using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System;
using Android.Content;

using CC;

namespace UI
{
    [Activity(Label = "KledingadviesOogActivity")]
    public class KledingadviesOogUI : Activity
    {
        // init KledingadviesBeheer object
        private KledingadviesBeheer kledingadviesBeheer = new KledingadviesBeheer();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // vangt kledingadvieskeuze array op
            string[] kledingadviesKeuze = Intent.GetStringArrayExtra("kledingadviesKeuze");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.KledingadviesOog);

            // init
            TextView textView1 = FindViewById<TextView>(Resource.Id.textView1);

            ImageButton btn_blue_eyes = FindViewById<ImageButton>(Resource.Id.btn_blue_eyes);
            ImageButton btn_brown_eyes = FindViewById<ImageButton>(Resource.Id.btn_brown_eyes);
            ImageButton btn_green_eyes = FindViewById<ImageButton>(Resource.Id.btn_green_eyes);
            ImageButton btn_grey_eyes = FindViewById<ImageButton>(Resource.Id.btn_grey_eyes);
            ImageButton btn_black_eyes = FindViewById<ImageButton>(Resource.Id.btn_black_eyes);

            btn_blue_eyes.Click += delegate {
                kledingadviesKeuze[0] = "blue";
                Btn_click(kledingadviesKeuze);
            };

            btn_brown_eyes.Click += delegate {
                kledingadviesKeuze[0] = "brown";
                Btn_click(kledingadviesKeuze);
            };

            btn_grey_eyes.Click += delegate {
                kledingadviesKeuze[0] = "grey";
                Btn_click(kledingadviesKeuze);
            };

            btn_green_eyes.Click += delegate {
                kledingadviesKeuze[0] = "green";
                Btn_click(kledingadviesKeuze);
            };

            btn_black_eyes.Click += delegate {
                kledingadviesKeuze[0] = "black";
                Btn_click(kledingadviesKeuze);
            };
        }

        private void Btn_click(string[] kledingadviesKeuze)
        {
            var kledingadviesHaarUI = new Intent(this, typeof(KledingadviesHaarUI));
            kledingadviesHaarUI.PutExtra("kledingadviesKeuze", kledingadviesKeuze);
            StartActivity(kledingadviesHaarUI);
            this.Finish();
        }
    }
}



