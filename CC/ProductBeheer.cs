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

using BU;

namespace CC
{
    public class ProductBeheer
    {
        public ProductBeheer()
        {
            createDatabase();
        }

        private void createDatabase()
        {
            // To Do: Check database content & delete and rebuild if not valid
            // creates TABLE if TABLE Product doesn't exist
            bool tableExists = checkTableExists();

            if (!tableExists)
                createTable();
        }

        private string getDatabasePath()
        {
            // string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            string filenamePath = System.IO.Path.Combine(path, "sqlite.db");

            return filenamePath;
        }

        // checks if table Product exists
        private bool checkTableExists()
        {
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
            {
                List<Product> producten = database.Query<Product>("SELECT * FROM sqlite_master WHERE type = 'table' AND name = 'Product'");
                bool tableExists = producten.Count > 0;

                return tableExists;
            }
        }

        // creates Product TABLE
        private void createTable()
        {
            using (var conn = new SQLite.SQLiteConnection(getDatabasePath()))
                conn.CreateTable<Product>();
        }

        // voegt product toe aan db
        public void ToevoegenProduct(string[] productgegevens)
        {
            var product = new Product { Naam = productgegevens[0], Afbeelding = productgegevens[1], Prijs = productgegevens[2], Korting = productgegevens[3], Categorie = productgegevens[4] };
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
                database.Insert(product);
        }

        // checks if table Product exists
        public List<string> findOngekoppeldeProducten()
        {
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
            {
                List<string> lijstongekoppeldeproducten = new List<string>();

                List<Product> ongekoppeldeproducten = database.Query<Product>("SELECT * FROM PRODUCT WHERE GEKOPPELD is null OR GEKOPPELD = ''");

                for (int i = 0; i < ongekoppeldeproducten.Count; i++)
                    lijstongekoppeldeproducten.Add(ongekoppeldeproducten[i].Naam);

                //string[] arrayOngekoppeldeproducten = lijstongekoppeldeproducten.ToArray();

                //return arrayOngekoppeldeproducten;
                return lijstongekoppeldeproducten;
            }
        }

        // insertGekoppeldKledingadvies
        public void UpdateGekoppeldProduct(string product, string seizoenstype, string lichaamstype)
        {
            // zoek product & update

            // update product in database met toeveogingen van seizoenstype, lichaamstype en gekoppeldwaardestatus
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
            {
                string gekoppeldWaarde = "true";
                database.Execute("UPDATE PRODUCT SET SEIZOENSTYPE = ?, LICHAAMSTYPE = ?, GEKOPPELD = ? WHERE NAAM = ?", seizoenstype, lichaamstype, gekoppeldWaarde, product);
            }
                
        }
    }
}