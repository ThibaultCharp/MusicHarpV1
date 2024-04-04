using DataLogicLayer.Entitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer.DAL
{
	public class SongRepository
	{
		DatabaseConnection _dbConnection = new DatabaseConnection();





		public List<Song> GetAllSongs()
		{
			var songs = new List<Song>();

			if (_dbConnection.OpenConnection())
			{
				var query = "SELECT `id`, `name`, `song_url` FROM `songs`";

				using (var command = new MySqlCommand(query, _dbConnection.connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var song = new Song
							{
								Id = Convert.ToInt32(reader["Id"]),
								Name = reader["Name"].ToString(),
								SongUrl = reader["Song_url"].ToString(),
								// Map other properties as needed
							};
							songs.Add(song);
						}
					}
				}

				_dbConnection.CloseConnection(); // Close the connection after retrieving data
			}

			return songs;
		}

	}
}
