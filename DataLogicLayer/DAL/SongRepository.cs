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
                var query = "SELECT songs.name AS song_name, songs.song_url, artists.name AS artist_name " +
                            "FROM songs " +
                            "JOIN artist_songs ON songs.id = artist_songs.song_id " +
                            "JOIN artists ON artist_songs.artist_id = artists.id";

                using (var command = new MySqlCommand(query, _dbConnection.connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Artist artist = new Artist();
                            var song = new Song
                            {
                                
                                SongName = reader["song_name"].ToString(),
                                ArtistName = reader["artist_name"].ToString(),
                                SongUrl = reader["song_url"].ToString(),
                            };
                            songs.Add(song);
                        }
                    }
                }
                _dbConnection.CloseConnection();
			}

			return songs;
		}

	}
}
