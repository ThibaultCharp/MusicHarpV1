using DataLogicLayer.Entitys;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer.DAL
{
	public class PlaylistRepository
	{

		DatabaseConnection _dbConnection = new DatabaseConnection();

		public List<Playlist> GetSelectedPlaylists()
		{
			var playlists = new List<Playlist>();

			if (_dbConnection.OpenConnection())
			{
				string query = "SELECT * FROM `playlists` ORDER BY id DESC";

				using (var command = new MySqlCommand(query, _dbConnection.connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var playlist = new Playlist
							{
								Id = Convert.ToInt32(reader["id"]),
								Name = reader["name"].ToString(),
							};
							playlists.Add(playlist);
						}
					}
				}
				_dbConnection.CloseConnection();
			}
			return playlists;
		}



		public void CreateNewPlaylist(Playlist playlist)
		{
			string query = "INSERT INTO `playlists` (`id`, `name`, `user_id`) VALUES (NULL, @Name, '5');";
			using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=musicharp_db;UID=root;PASSWORD="))
			{
				connection.Open();
				using (var cmd = new MySqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("@Name", playlist.Name);
					cmd.ExecuteNonQuery();
				}
				_dbConnection.CloseConnection();
			}
		}

		public void DeletePlaylist(int id)
		{
			string query1 = "DELETE FROM playlists WHERE id = @id";
			string query2 = "DELETE FROM playlist_songs WHERE playlist_id = @id";
			using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=musicharp_db;UID=root;PASSWORD="))
			{
				connection.Open();

				using (var cmd = new MySqlCommand(query2, connection))
				{
					cmd.Parameters.AddWithValue("@id", id);
					cmd.ExecuteNonQuery();
				}

				using (var cmd = new MySqlCommand(query1, connection))
				{
					cmd.Parameters.AddWithValue("@id", id);
					cmd.ExecuteNonQuery();
				}
				_dbConnection.CloseConnection();
			}

		}

		public Playlist GetWantedPlaylist(int id)
		{
			string query = "SELECT id, name FROM playlists WHERE id = @id";
			Playlist playlist = new Playlist();

			using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=musicharp_db;UID=root;PASSWORD="))
			{
				connection.Open();

				using (var cmd = new MySqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("@id", id);
					cmd.ExecuteNonQuery();
					MySqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						playlist.Id = Convert.ToInt32(reader["id"]);
						playlist.Name = reader["name"].ToString();
					}
					reader.Close();
				}
			}

			return playlist;
		}


		public void SaveEditedPlaylist(Playlist playlist)
		{
			string query = "UPDATE playlists SET name = @name WHERE id = @id";
			using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=musicharp_db;UID=root;PASSWORD="))
			{
				connection.Open();

				using (var cmd = new MySqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("@name", playlist.Name);
					cmd.Parameters.AddWithValue("@id", playlist.Id);
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}