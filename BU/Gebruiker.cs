using System;
using SQLite;

namespace BU
{
	public class Gebruiker
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Mailadres { get; set; }
		public string Wachtwoord { get; set; }
		public string Rol {get;set;}
	}
}

