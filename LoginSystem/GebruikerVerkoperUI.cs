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
    [Activity(Label = "GebruikerVerkoperUI")]
    public class GebruikerVerkoperUI : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GebruikerVerkoper);

            // init
            Button btnScanQRCode = FindViewById<Button>(Resource.Id.btnScanQRCode);
            Button btnTerugNaarMain = FindViewById<Button>(Resource.Id.btnTerugNaarMain);
            
            btnScanQRCode.Click += delegate {
                StartActivity(typeof(ScanQRCodeUI));
            };

            btnTerugNaarMain.Click += delegate {
                this.Finish();
            };
        }
    }
}