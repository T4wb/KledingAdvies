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
    [Activity(Label = "Tonen Kledingadvies")]
    public class TonenKledingadviesUI : Activity
    {
        private string categorie;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TonenKledingadvies);

            // spinner
            ImageButton imageButtonPlaceholder = FindViewById<ImageButton>(Resource.Id.imageButtonPlaceholder);
            Button btnZoekProducten = FindViewById<Button>(Resource.Id.btnZoekProducten);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner_categorieproduct);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.categorieArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            btnZoekProducten.Click += delegate
            {
                // To Do: pass Categorie & other data to productenbeheer => connection with sqlite... results: products
                // For now:
                imageButtonPlaceholder.Visibility = ViewStates.Visible;
            };

            imageButtonPlaceholder.Click += delegate
            {
                // starts QR Generen Activity
                imageButtonPlaceholder.Visibility = ViewStates.Invisible;
                this.Finish();
                StartActivity(typeof(GenereerQRCodeUI));
            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // init
            Spinner spinner = (Spinner)sender;

            categorie = spinner.GetItemAtPosition(e.Position).ToString();
        }
    }
}