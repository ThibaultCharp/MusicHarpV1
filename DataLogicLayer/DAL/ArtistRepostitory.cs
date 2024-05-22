using BusinessLogicLayer.EntityDTO_s;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Repo_Interfaces;
using System.Xml.Linq;

namespace DataLogicLayer.DAL
{
    public class ArtistRepostitory : IArtistRepository
    {
        DatabaseConnection _dbConnection = new DatabaseConnection();


        public List<ArtistDTO> GetAllArtists()
        {
            var artists = new List<ArtistDTO>();
            string query = "SELECT `id`, `name` FROM `artists` ORDER BY id DESC";


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
                                ArtistDTO artist = new ArtistDTO
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Name = reader["name"].ToString(),
                                };
                                artists.Add(artist);
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

            return artists;

        }

        public void CreateNewArtist(ArtistDTO artist)
        {
            string query = "INSERT INTO `artists`(`id`, `name`) VALUES (NULL,@Name)";
            try
            {
                if (_dbConnection.OpenConnection())
                {
                    using (var command = new MySqlCommand(query, _dbConnection.connection))
                    {
                        command.Parameters.AddWithValue("@Name", artist.Name);
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

        public List<SongDTO> SongsFromArtist(int id)
        {
            List<SongDTO> songs = new List<SongDTO>();

            string query = "SELECT artist_songs.artist_id, songs.id AS song_id, songs.name AS song_name, songs.song_url " +
                           "FROM artist_songs " +
                           "JOIN songs ON artist_songs.song_id = songs.id " +
                           "WHERE artist_songs.artist_id = @id";

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
