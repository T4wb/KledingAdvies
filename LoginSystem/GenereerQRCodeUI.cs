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
    [Activity(Label = "GenereerQRCodeUI")]
    public class GenereerQRCodeUI : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GenereerQRCode);

            // init
            ImageView imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
        }
    }
}