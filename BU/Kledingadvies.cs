using System;
using SQLite;

namespace BU
{
    public class Kledingadvies
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Oogkleur { get; set; }
        public string Haarkleur { get; set; }
        public string Huidskleur { get; set; }
        public string Lichaamstype { get; set; }
        public string Seizoenstype { get; set; }
    }
}