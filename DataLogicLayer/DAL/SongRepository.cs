using DataLogicLayer.Entitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Media;

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
                var query = "SELECT songs.name AS song_name, songs.song_url, artists.name AS artist_name, songs.id " +
                    "FROM songs JOIN artist_songs ON songs.id = artist_songs.song_id " +
                    "JOIN artists ON artist_songs.artist_id = artists.id " +
                    "ORDER BY songs.id DESC";

                using (var command = new MySqlCommand(query, _dbConnection.connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Song song = new Song
                            {
                                Id = Convert.ToInt32(reader["id"]),
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



        public List<Song> GetSearchedSongs(string input)
        {
            var songs = new List<Song>();

            if (_dbConnection.OpenConnection())
            {
                string query = "SELECT songs.name AS song_name, artists.name AS artist_name, songs.song_url, songs.id " +
                               "FROM songs, artists, artist_songs " +
                               "WHERE songs.id = artist_songs.song_id " +
                               "AND artists.id = artist_songs.artist_id " +
                               "AND (songs.name LIKE @Input OR artists.name LIKE @Input)" +
                               "ORDER BY songs.id DESC";
                
                using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=musicharp_db;UID=root;PASSWORD="))
                {
                    connection.Open();
                    var cmd = new MySqlCommand(query, connection);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@Input", $"%{input}%");
                        cmd.ExecuteNonQuery();
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Song song = new Song
                            {
                                SongName = reader["song_name"].ToString(),
                                ArtistName = reader["artist_name"].ToString(),
                                SongUrl = reader["song_url"].ToString(),
                                Id = Convert.ToInt32(reader["id"])

                            };
                            songs.Add(song);
                        }
                    }
                    _dbConnection.CloseConnection();
                }
            }
            return songs;
        }


        public void InsertSongInPlaylist(int pId, int sId)
        {
            string query = "INSERT INTO `playlist_songs` (`id`, `playlist_id`, `song_id`) VALUES (NULL, @pId, @sId)";

            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=musicharp_db;UID=root;PASSWORD="))
            {
                connection.Open();
                var cmd = new MySqlCommand(query, connection);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@sId", sId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}