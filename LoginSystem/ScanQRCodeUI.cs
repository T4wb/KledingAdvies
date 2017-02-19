using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ZXing.Mobile;

namespace UI
{
    [Activity(Label = "ScanQRCodeActivity")]
    public class ScanQRCodeUI : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QRScannen);
            TextView output = FindViewById<TextView>(Resource.Id.txt_output);


            // Initialize the scanner first so it can track the current context
            MobileBarcodeScanner.Initialize(Application);
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            //Tell our scanner to use the default overlay
            scanner.UseCustomOverlay = false;

            //We can customize the top and bottom text of the default overlay
            scanner.TopText = "Houd de camera op korte afstand van de QR-code";
            scanner.BottomText = "Wacht tot de code automatisch gescand wordt!";
            var result = await scanner.Scan();
            HandleScanResult(result);


            //if (result != null)
            //    {
            //        output.Text = ("Scannen voltooid: " + result.Text);
            //    }
        }

        void HandleScanResult(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
                msg = "Barcode gevonden " + result.Text;
            else
                msg = "Fout bij het scannen! Probeer het nog eens.";

            this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
        }
    }
}
