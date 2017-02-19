using System;
using System.IO;
using System.Collections.Generic;

using BU;

namespace CC
{
    public class Verkopersbeheer
    {
        public Verkopersbeheer()
        {
            createDatabase();
        }

        private void createDatabase()
        {
            // To Do: Check database content & delete and rebuild if not valid
            // creates TABLE if TABLE Verkoper doesn't exists
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

        // checks if TABLE Verkoper exists
        private bool checkTableExists()
        {
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
            {
                List<Verkoper> verkopers = database.Query<Verkoper>("SELECT * FROM sqlite_master WHERE type = 'table' AND name = 'Verkoper'");
                bool tableExists = verkopers.Count > 0;

                return tableExists;
            }
        }

        private bool checkTableGebruikerExists()
        {
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
            {
                List<Gebruiker> gebruikers = database.Query<Gebruiker>("SELECT * FROM sqlite_master WHERE type = 'table' AND name = 'Gebruiker'");
                bool tableExists = gebruikers.Count > 0;

                return tableExists;
            }
        }

        // creates Verkoper TABLE
        private void createTable()
        {
            using (var conn = new SQLite.SQLiteConnection(getDatabasePath()))
                conn.CreateTable<Verkoper>();
        }

        public void ToevoegenVerkoper(string emailVerkoper, string emailOndernemer)
        {
            var verkopers = new Verkoper { EmailVerkoper = emailVerkoper, EmailOndernemer = emailOndernemer };
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
                database.Insert(verkopers);
        }

        public string CheckVerkoper(string email)
        {
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
            {
                if (!checkTableGebruikerExists())
                    return "Deze verkoper bestaat niet";

                List<Gebruiker> verkopers = database.Query<Gebruiker>("SELECT * FROM GEBRUIKER WHERE Mailadres = ? AND Rol = 'Verkoper'", email);
                if (verkopers.Count <= 0)
                    return "Deze verkoper bestaat niet";

                Gebruiker accountFound = verkopers[0];
                string emailVerkoper = accountFound.Mailadres;

                return emailVerkoper;
            }
        }
    }
}