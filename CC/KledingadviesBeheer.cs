using System;
using System.IO;
using System.Collections.Generic;

using BU;

namespace CC
{
    public class KledingadviesBeheer
    {
        public KledingadviesBeheer()
        {
            createDatabase();
        }

        private void createDatabase()
        {
            // To Do: Check database content & delete and rebuild if not valid
            // creates TABLE if database exists but TABLE Kledingadvies doesn't or if database doesn't exist;
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

        // checks if table Kledingadvies exists
        private bool checkTableExists()
        {
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
            {
                List<Kledingadvies> kledingadviezen = database.Query<Kledingadvies>("SELECT * FROM sqlite_master WHERE type = 'table' AND name = 'Kledingadvies'");
                bool tableExists = kledingadviezen.Count > 0;

                return tableExists;
            }
        }

        // creates Kledingadvies TABLE
        private void createTable()
        {
            using (var conn = new SQLite.SQLiteConnection(getDatabasePath()))
                conn.CreateTable<Kledingadvies>();
        }

        // insertKledingKeuze
        public void insertKledingKeuze(string[] kledingadviesKeuze)
        {
            // voegt seizoenstype aan kledingadviesKeuze string[]
            kledingadviesKeuze = bepalenSeizoenstype(kledingadviesKeuze);

            // schrijft kledingadvies naar database
            var kledingadvies = new Kledingadvies { Oogkleur = kledingadviesKeuze[0], Haarkleur = kledingadviesKeuze[1], Huidskleur = kledingadviesKeuze[2], Lichaamstype = kledingadviesKeuze[3], Seizoenstype = kledingadviesKeuze[4] };
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
                database.Insert(kledingadvies);
        }

        private string[] bepalenSeizoenstype(string[] kledingadviesKeuze)
        {
            // To Do: Refactor naar verwerkOogkleur, verwerkHaarkleur, verwerkHuidskleur
            // kan mogelijk anders: List/Array?

            int lente = 0;
            int zomer = 0;
            int herfst = 0;
            int winter = 0;

            // kan mogelijk anders: List/Array?
            List<int> SeasonsList = new List<int>();

            // init
            string oogkleur = kledingadviesKeuze[0];
            string haarkleur = kledingadviesKeuze[1];
            string huidskleur = kledingadviesKeuze[2];
            string lichaamstype = kledingadviesKeuze[3];
            string typeSeizoen = "";

            // oog type bepaling
            if (oogkleur == "blue" || oogkleur == "grey" || oogkleur == "green")
            {
                lente++;
                zomer++;
            }
            else if (oogkleur == "brown")
            {
                winter++;
                herfst++;
            }
            else if (oogkleur == "black")
                winter++;

            // haar type bepaling
            if (haarkleur == "light_blond" || haarkleur == "medium_blond" || haarkleur == "dark_blond" || haarkleur == "light_brown")
            {
                zomer++;
                lente++;
            }
            else if (haarkleur == "medium_brown")
            {
                zomer++;
                lente++;
                herfst++;
            }
            else if (haarkleur == "dark_brown")
            {
                winter++;
                herfst++;
            }
            else if (haarkleur == "light_grey" || haarkleur == "dark_grey" || haarkleur == "black")
                winter++;

            // huidskleur bepaling
            if (huidskleur == "brown")
            {
                lente++;
                zomer++;
            }
            else if (huidskleur == "white")
            {
                winter++;
                herfst++;
            }
            else if (huidskleur == "black")
                zomer++;

            // eind type bepaling
            SeasonsList.Add(lente);
            SeasonsList.Add(zomer);
            SeasonsList.Add(herfst);
            SeasonsList.Add(winter);

            int type = 0;
            foreach (int seizoenswaarde in SeasonsList)
            {
                if (seizoenswaarde > type)
                    type = seizoenswaarde;
            }

            // zet string typeSeizoen
            if (type == lente)
                typeSeizoen = "Lente";
            else if (type == zomer)
                typeSeizoen = "Zomer";
            else if (type == herfst)
                typeSeizoen = "Herfst";
            else if (type == winter)
                typeSeizoen = "Winter";
            kledingadviesKeuze[4] = typeSeizoen;

            return kledingadviesKeuze;
        }

        /*
        // kan misschien later gebruikt worden om gegevens te halen
        // getLaatsteKledingadvies
        public string getWaardeDatabase(string typeWaarde)
        {
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
            {
                string waarde = "";

                List<Kledingadvies> kledingadvies = database.Query<Kledingadvies>("SELECT * FROM KLEDINGADVIES");
                Kledingadvies recentAdvies = kledingadvies[kledingadvies.Count - 1];

                if (typeWaarde == "Oogkleur")
                    waarde = recentAdvies.Oogkleur;
                else if (typeWaarde == "Haarkleur")
                    waarde = recentAdvies.Haarkleur;
                else if (typeWaarde == "Huidskleur")
                    waarde = recentAdvies.Huidskleur;
                else if (typeWaarde == "Seizoenstype")
                    waarde = recentAdvies.Seizoenstype;

                return waarde;
            }
        }*/

    }
}
