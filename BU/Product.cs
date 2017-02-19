using System;
using SQLite;

namespace BU
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Naam { get; set; }
        public string Afbeelding { get; set; }
        public string Prijs { get; set; }
        public string Korting { get; set; }
        public string Categorie { get; set; }
        public string Seizoenstype { get; set; }
        public string Lichaamstype { get; set; }
        public string Gekoppeld { get; set; }

    }
}