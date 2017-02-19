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
    [Activity(Label = "KoppelenKledingadviesUI")]
    public class KoppelenProductUI : Activity
    {
        private string product;
        private string seizoenstype;
        private string lichaamstype;

        // init productadviesBeheer object
        private ProductBeheer productBeheer = new ProductBeheer();

        private List<string> ongekoppeldeproducten = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.KoppelenProduct);

            // init 
            Spinner spinnerProduct = FindViewById<Spinner>(Resource.Id.spinnerProduct);
            Spinner spinnerSeizoenstype = FindViewById<Spinner>(Resource.Id.spinnerSeizoenstype);
            Spinner spinnerLichaamstype = FindViewById<Spinner>(Resource.Id.spinnerLichaamstype);

            Button btnKoppelenKledingadvies = FindViewById<Button>(Resource.Id.btnKoppelenKledingadvies);
            Button btnTerugNaarMain = FindViewById<Button>(Resource.Id.btnTerugNaarMain);

            // spinnerProduct
            // hier worden de ongekoppelde producten opgehaald en als een lijst toegewezen aan de spinner items van spinnerProduct
            ongekoppeldeproducten = productBeheer.findOngekoppeldeProducten();

            spinnerProduct.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, ongekoppeldeproducten);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerProduct.Adapter = adapter;

            // spinnerSeizoenstype
            spinnerSeizoenstype.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected2);
            var adapter2 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.seizoenstypeArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerSeizoenstype.Adapter = adapter2;

            // spinnerLichaamstype
            spinnerLichaamstype.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected3);
            var adapter3 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.lichaamstypeArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerLichaamstype.Adapter = adapter3;

            btnKoppelenKledingadvies.Click += delegate {
                productBeheer.UpdateGekoppeldProduct(product, seizoenstype, lichaamstype);
                Toast.MakeText(this.BaseContext, "Het Koppelen is gelukt", ToastLength.Short).Show();
                this.Finish();
            };

            btnTerugNaarMain.Click += delegate {
                this.Finish();
            };

        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // init
            Spinner spinner = (Spinner)sender;

            product = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void spinner_ItemSelected2(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // init
            Spinner spinner = (Spinner)sender;

            seizoenstype = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void spinner_ItemSelected3(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // init
            Spinner spinner = (Spinner)sender;

            lichaamstype = spinner.GetItemAtPosition(e.Position).ToString();
        }
    }
}