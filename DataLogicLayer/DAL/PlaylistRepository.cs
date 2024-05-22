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
using System.Xml.Linq;
using BusinessLogicLayer.ErrorHandling;


namespace DataLogicLayer.DAL
{
	public class PlaylistRepository : IPlaylistRepository
	{
		DatabaseConnection _dbConnection = new DatabaseConnection();

		public List<PlaylistDTO> GetSelectedPlaylists(int? user_id)
		{
			var playlists = new List<PlaylistDTO>();
            string query = "SELECT * FROM `playlists` WHERE @User_id = playlists.user_id ORDER BY playlists.id DESC";

            try
            {
                if (_dbConnection.OpenConnection())
                {
                    using (var command = new MySqlCommand(query, _dbConnection.connection))
                    {
                        command.Parameters.AddWithValue("@User_id", user_id);

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
                }
            }

            catch (DatabaseErrorExeption ex)
            {
                throw;
            }

            finally
            {
                _dbConnection.CloseConnection();
            }

            return playlists;
		}

        public PlaylistDTO GetWantedPlaylist(int id, int? user_id)
        {
            PlaylistDTO playlist = new PlaylistDTO();
            string query = "SELECT playlists.id, name FROM playlists WHERE @User_id = playlists.user_id";
            try
            {
                if (_dbConnection.OpenConnection())
                {
                    using (var command = new MySqlCommand(query, _dbConnection.connection))
                    {
                        command.Parameters.AddWithValue("@User_id", user_id);
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
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex);
            }

            finally
            {
                _dbConnection.CloseConnection();
            }
            return playlist;
        }


        public void CreateNewPlaylist(PlaylistDTO playlist, int? user_id)
		{
            try
            {
                if (_dbConnection.OpenConnection())
                {
                    string query = "INSERT INTO `playlists` (`id`, `name`, `user_id`) VALUES (NULL, @Name, @User_id)";

                    using (var command = new MySqlCommand(query, _dbConnection.connection))
                    {
                        command.Parameters.AddWithValue("@Name", playlist.Name);
                        command.Parameters.AddWithValue("@User_id", user_id);
                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex);
            }

            finally
            {
                _dbConnection.CloseConnection();
            }
        }

		public void DeletePlaylist(int id)
		{
            try
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
                }
            }

            catch (Exception ex) 
            {
                Console.WriteLine("An error occured: " + ex);
            }

            finally
            {
                _dbConnection.CloseConnection();
            }
        }




		public void SaveEditedPlaylist(PlaylistDTO playlist)
		{
            string query = "UPDATE playlists SET name = @name WHERE id = @id";
            try
            {
                if (_dbConnection.OpenConnection())
                {

                    using (var command = new MySqlCommand(query, _dbConnection.connection))
                    {
                        command.Parameters.AddWithValue("@name", playlist.Name);
                        command.Parameters.AddWithValue("@id", playlist.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex);
            }

            finally
            {
                _dbConnection.CloseConnection();
            }
        }


        public List<SongDTO> GetSongsInPlaylist(int id)
        {
            List<SongDTO> songs = new List<SongDTO>();

            string query = "SELECT songs.name AS song_name, songs.song_url, artists.name AS artist_name, songs.id " +
                               "FROM songs " +
                               "JOIN artist_songs ON songs.id = artist_songs.song_id " +
                               "JOIN artists ON artist_songs.artist_id = artists.id " +
                               "JOIN playlist_songs ON songs.id = playlist_songs.song_id " + 
                               "WHERE playlist_songs.playlist_id = @id";

            try
            {
                if (_dbConnection.OpenConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(query, _dbConnection.connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        SongDTO song = new SongDTO();
                        song.SongName = dataReader["song_name"].ToString();
                        song.SongUrl = dataReader["song_url"].ToString();
                        song.ArtistName = dataReader["artist_name"].ToString();
                        songs.Add(song);
                    }
                    dataReader.Close();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex);
            }

            finally 
            {
                _dbConnection.CloseConnection();
            }
            return songs;
        }
    }
}