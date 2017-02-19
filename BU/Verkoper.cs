using System;
using SQLite;

namespace BU
{
    public class Verkoper
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string EmailVerkoper { get; set; }
        public string EmailOndernemer { get; set; }
    }
}