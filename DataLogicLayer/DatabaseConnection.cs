using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer
{
	public class DatabaseConnection
	{
		public MySqlConnection connection;
		private string server;
		private string database;
		private string uid;
		private string password;


		public DatabaseConnection()
		{
			Initialize();
		}


		private void Initialize()
		{
			server = "127.0.0.1";
			database = "musicharp_db";
			uid = "root";
			password = "";
			string connectionString;
			connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

			connection = new MySqlConnection(connectionString);
		}

		// Open connection to database
		public bool OpenConnection()
		{
			try
			{
				connection.Open();
				return true;
			}
			catch (MySqlException ex)
			{

				return false;
			}
		}

		// Close connection to database
		public bool CloseConnection()
		{
			try
			{
				connection.Close();
				return true;
			}
			catch (MySqlException ex)
			{
				return false;
			}
		}
	}
}