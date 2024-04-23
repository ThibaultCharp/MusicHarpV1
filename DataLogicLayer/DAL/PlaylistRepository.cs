using MySql.Data.MySqlClient;
using BusinessLogicLayer.EntityDTO_s;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Repo_Interfaces;


namespace DataLogicLayer.DAL
{
	public class PlaylistRepository : IPlaylistRepository
	{
		DatabaseConnection _dbConnection = new DatabaseConnection();

		public List<PlaylistDTO> GetSelectedPlaylists()
		{
			var playlists = new List<PlaylistDTO>();

			if (_dbConnection.OpenConnection())
			{
				string query = "SELECT * FROM `playlists` ORDER BY id DESC";

				using (var command = new MySqlCommand(query, _dbConnection.connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var playlist = new PlaylistDTO
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

        public PlaylistDTO GetWantedPlaylist(int id)
        {
            PlaylistDTO playlist = new PlaylistDTO();

            if (_dbConnection.OpenConnection())
            {
                string query = "SELECT id, name FROM playlists WHERE id = @id";

                using (var command = new MySqlCommand(query, _dbConnection.connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playlist.Id = Convert.ToInt32(reader["id"]);
                            playlist.Name = reader["name"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            return playlist;
        }


        public void CreateNewPlaylist(PlaylistDTO playlist)
		{
            if (_dbConnection.OpenConnection())
			{
                string query = "INSERT INTO `playlists` (`id`, `name`, `user_id`) VALUES (NULL, @Name, '5')";
                using (var command = new MySqlCommand(query, _dbConnection.connection))
				{
                    command.Parameters.AddWithValue("@Name", playlist.Name);
                    command.ExecuteNonQuery();
                }
				_dbConnection.CloseConnection();
            }
        }

		public void DeletePlaylist(int id)
		{

            if (_dbConnection.OpenConnection())
			{
                string query1 = "DELETE FROM playlist_songs WHERE playlist_id = @id";
                string query2 = "DELETE FROM playlists WHERE id = @id";

                using (var command = new MySqlCommand(query1, _dbConnection.connection))
				{
					command.Parameters.AddWithValue("@id", id);
					command.ExecuteNonQuery();
				}

                using (var command = new MySqlCommand(query2, _dbConnection.connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                _dbConnection.CloseConnection();
            }
        }




		public void SaveEditedPlaylist(PlaylistDTO playlist)
		{

            if (_dbConnection.OpenConnection())
            {
                string query = "UPDATE playlists SET name = @name WHERE id = @id";

                using (var command = new MySqlCommand(query, _dbConnection.connection))
                {
                    command.Parameters.AddWithValue("@name", playlist.Name);
                    command.Parameters.AddWithValue("@id", playlist.Id);
                    command.ExecuteNonQuery();
                }
                _dbConnection.CloseConnection();
            }
        }


        public List<SongDTO> GetSongsInPlaylist(int id)
        {
            List<SongDTO> songs = new List<SongDTO>();

            string query = "SELECT name, song_url " +
                            "FROM songs " +
                            "JOIN playlist_songs ON songs.id = playlist_songs.song_id " +
                            "WHERE playlist_songs.playlist_id = @id";
			if (_dbConnection.OpenConnection())
            {

                MySqlCommand cmd = new MySqlCommand(query, _dbConnection.connection);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    SongDTO song = new SongDTO();
                    song.SongName = dataReader["name"].ToString();
                    song.SongUrl = dataReader["song_url"].ToString();
					
                    songs.Add (song);
                }
                dataReader.Close();
                _dbConnection.CloseConnection();
            }
            return songs;
        }
    }
}