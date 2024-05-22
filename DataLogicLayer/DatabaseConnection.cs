using Microsoft.AspNetCore.Hosting.Server;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer
{
	public class DatabaseConnection
	{
		public MySqlConnection connection;



		public DatabaseConnection()
		{
			Initialize();
		}


		private void Initialize()
		{

            string connectionString; 
            connectionString = "Server = studmysql01.fhict.local; Uid = dbi538679; Database = dbi538679; Password = Nsp3lEXftR;";

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