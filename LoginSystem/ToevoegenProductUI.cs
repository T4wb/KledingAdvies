using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using CC;
using QRcode;

namespace UI
{
    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }

    [Activity(Label = "ToevoegenProductUI")]
    public class ToevoegenProductUI : Activity
    {
        private ImageView _imageView;
        //private Bitmap = new GebruikersBeheer();

        // init KledingadviesBeheer object
        private ProductBeheer productBeheer = new ProductBeheer();

        string categorie;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(200, 300);
            if (App.bitmap != null)
            {
                _imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ToevoegenProductUI);

            Boolean foto_gemaakt = false;

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.categorieArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            EditText naam = FindViewById<EditText>(Resource.Id.inv_naam);
            EditText prijs = FindViewById<EditText>(Resource.Id.inv_prijs);
            EditText korting = FindViewById<EditText>(Resource.Id.inv_korting);
            Button opslaan = FindViewById<Button>(Resource.Id.btn_opslaan);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                Button button = FindViewById<Button>(Resource.Id.btn_open_camera);
                _imageView = FindViewById<ImageView>(Resource.Id.imageView1);

                button.Click += delegate
                {
                    Intent intent = new Intent(MediaStore.ActionImageCapture);
                    string bestandsnaam = String.Format("afbeelding{0}.jpg", Guid.NewGuid());
                    App._file = new File(App._dir, bestandsnaam);
                    intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
                    StartActivityForResult(intent, 0);
                    foto_gemaakt = true;
                };

            }

            opslaan.Click += delegate
            {
                if (!(string.IsNullOrWhiteSpace(naam.Text) && string.IsNullOrWhiteSpace(prijs.Text)) && foto_gemaakt)
                {
                    decimal prijs_afgerond = Math.Round(decimal.Parse(prijs.Text), 2);
                    decimal korting_afgerond = 0;

                    if (!string.IsNullOrEmpty(korting.Text))
                        korting_afgerond = Math.Round(decimal.Parse(korting.Text), 2);

                    //decimal korting_afgerond = Math.Round(decimal.Parse(korting.Text), 2);

                    if (prijs_afgerond - korting_afgerond > 0)
                    {
                        string prijs_string = Convert.ToString(prijs_afgerond);
                        string korting_string = Convert.ToString(korting_afgerond);

                        string[] productgegevens = new string[5];
                        productgegevens[0] = naam.Text;
                        productgegevens[1] = "AfbPathToDo";
                        productgegevens[2] = prijs_string;
                        productgegevens[3] = korting_string;
                        productgegevens[4] = korting_string;
                        productgegevens[4] = categorie;

                        productBeheer.ToevoegenProduct(productgegevens);
                        Toast.MakeText(this, "Het product is toegevoegd", ToastLength.Long).Show();
                        this.Finish();
                    }    
                }

                else
                {
                    Toast.MakeText(this, "Niet alle verplichte velden zijn ingevoerd!", ToastLength.Long).Show();
                }
            };

        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "Kledingadvies");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // init
            Spinner spinner = (Spinner)sender;

            categorie = spinner.GetItemAtPosition(e.Position).ToString();

            //testing
            //Toast.MakeText(this, categorie, ToastLength.Long).Show();
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

    }
}