using System;
using System.IO;
using System.Collections.Generic;

using BU;

namespace CC
{
    public class GebruikersBeheer
    {
        public GebruikersBeheer()
        {
            createDatabase();
        }

        private void createDatabase()
        {
            // To Do: Check database content & delete and rebuild if not valid
            // creates database if it doesn't exist yet
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
                List<Gebruiker> gebruikers = database.Query<Gebruiker>("SELECT * FROM sqlite_master WHERE type = 'table' AND name = 'Gebruiker'");
                bool tableExists = gebruikers.Count > 0;

                return tableExists;
            }
        }

        private void createTable()
        {
            using (var conn = new SQLite.SQLiteConnection(getDatabasePath()))
                conn.CreateTable<Gebruiker>();
        }

        public bool RegistreerGebruiker(string email, string password, string rol)
        {
            bool userCreated = false;

            // checks if user already exists
            string userState = CheckVerificatie(email, password);

            if (userState == "Deze gebruiker bestaat niet")
            {
                // adds new record to database
                // To Do: Role = "gebruiker" should be initialized by parameter role - the argument is passed by method calling
                var person = new Gebruiker { Mailadres = email, Wachtwoord = password, Rol = rol };
                using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
                    database.Insert(person);

                userCreated = true;
            }

            return userCreated;
        }

        public string CheckVerificatie(string email, string password)
        {
            using (var database = new SQLite.SQLiteConnection(getDatabasePath()))
            {
                List<Gebruiker> persons = database.Query<Gebruiker>("SELECT * FROM GEBRUIKER WHERE Mailadres = ?", email);
                if (persons.Count <= 0)
                    return "Deze gebruiker bestaat niet";

                Gebruiker accountFound = persons[0];
                string userRole = accountFound.Rol;
                string realPassword = accountFound.Wachtwoord;

                // checks password
                if (password != realPassword)
                    return "Het ingevoerde wachtwoord is incorrect";

                return userRole;
            }
        }
    }
}

