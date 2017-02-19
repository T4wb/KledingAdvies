using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace UI
{
    [Activity(Label = "KAP", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Main);

            // init
            Button btnVaststellenKledingadvies = FindViewById<Button>(Resource.Id.btnVaststellenKledingadvies);
            Button btnTonenKledingadvies = FindViewById<Button>(Resource.Id.btnTonenKledingadvies);

            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);


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

            btnLogin.Click += delegate {
                StartActivity(typeof(LoginUserUI));
            };
        }
    }
}