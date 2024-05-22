using BusinessLogicLayer.EntityDTO_s;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using BusinessLogicLayer.Repo_Interfaces;

namespace DataLogicLayer.DAL
{
    public class SongRepository : ISongRepository
    {
        DatabaseConnection _dbConnection = new DatabaseConnection();

        public List<SongDTO> GetSongsWithoutArtist()
        {
            var songs = new List<SongDTO>();
            string query = "SELECT songs.id, songs.name AS song_name, songs.song_url " +
                            "FROM songs " +
                            "WHERE songs.id " +
                            "NOT IN (SELECT song_id FROM artist_songs) ORDER BY songs.name ASC ";

            try
            {
                if (_dbConnection.OpenConnection())
                {


                    using (var command = new MySqlCommand(query, _dbConnection.connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SongDTO song = new SongDTO
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    SongName = reader["song_name"].ToString(),
                                    SongUrl = reader["song_url"].ToString()
                                };
                                songs.Add(song);
                            }
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
            return songs;

        }

        public List<SongDTO> GetAllSongs()
        {
            var songs = new List<SongDTO>();
            var query = "SELECT songs.name AS song_name, songs.song_url, artists.name AS artist_name, songs.id " +
                        "FROM songs JOIN artist_songs ON songs.id = artist_songs.song_id " +
                        "JOIN artists ON artist_songs.artist_id = artists.id " +
                        "ORDER BY songs.name ASC";

            try
            {
                if (_dbConnection.OpenConnection())
                {


                    using (var command = new MySqlCommand(query, _dbConnection.connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SongDTO song = new SongDTO
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    SongName = reader["song_name"].ToString(),
                                    ArtistName = reader["artist_name"].ToString(),
                                    SongUrl = reader["song_url"].ToString()
                                };
                                songs.Add(song);
                            }
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
            return songs;


        }



        public List<SongDTO> GetSearchedSongs(string input)
        {
            var songs = new List<SongDTO>();
            string query = "SELECT songs.name AS song_name, artists.name AS artist_name, songs.song_url, songs.id " +
               "FROM songs, artists, artist_songs " +
               "WHERE songs.id = artist_songs.song_id " +
               "AND artists.id = artist_songs.artist_id " +
               "AND (songs.name LIKE @Input OR artists.name LIKE @Input)" +
               "ORDER BY songs.id DESC";

            try
            {
                if (_dbConnection.OpenConnection())
                {




                    var cmd = new MySqlCommand(query, _dbConnection.connection);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@Input", $"%{input}%");
                        cmd.ExecuteNonQuery();
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SongDTO song = new SongDTO
                            {
                                SongName = reader["song_name"].ToString(),
                                ArtistName = reader["artist_name"].ToString(),
                                SongUrl = reader["song_url"].ToString(),
                                Id = Convert.ToInt32(reader["id"])

                            };
                            songs.Add(song);
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
            return songs;
        }


        public void InsertSongInPlaylist(int pId, int sId)
        {
            string query = "INSERT INTO `playlist_songs` (`id`, `playlist_id`, `song_id`) VALUES (NULL, @pId, @sId)";

            try
            {
                _dbConnection.OpenConnection();
                var cmd = new MySqlCommand(query, _dbConnection.connection);
                using (cmd)
                {
                    string checkQuery = "SELECT COUNT(*) FROM `playlist_songs` WHERE `playlist_id` = @pId AND `song_id` = @sId";
                    var checkCmd = new MySqlCommand(checkQuery, _dbConnection.connection);
                    checkCmd.Parameters.AddWithValue("@pId", pId);
                    checkCmd.Parameters.AddWithValue("@sId", sId);

                    int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (existingCount == 0)
                    {
                        cmd.Parameters.AddWithValue("@pId", pId);
                        cmd.Parameters.AddWithValue("@sId", sId);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        Console.WriteLine("Song with ID {0} already exists in the playlist.", sId);
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


        public void LinkSongToArtist(int ArtistId, int SongId)
        {
            string query = "INSERT INTO `artist_songs`(`id`, `artist_id`, `song_id`) VALUES (NULL,@ArtistId,@SongId)";

            try
            {
                if (_dbConnection.OpenConnection())
                {
                    using (var cmd = new MySqlCommand(query, _dbConnection.connection))
                    {
                        cmd.Parameters.AddWithValue("@ArtistId", ArtistId);
                        cmd.Parameters.AddWithValue("@SongId", SongId);
                        cmd.ExecuteNonQuery();

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

        public void CreateNewSong(SongDTO song)
        {
            string query = "INSERT INTO `songs`(`id`, `name`, `song_url`) VALUES (NULL,@Name,@Url)";

            try
            {
                if (_dbConnection.OpenConnection())
                {
                    using (var cmd = new MySqlCommand(query, _dbConnection.connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", song.SongName);
                        cmd.Parameters.AddWithValue("@Url", song.SongUrl);
                        cmd.ExecuteNonQuery();
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
    }
}