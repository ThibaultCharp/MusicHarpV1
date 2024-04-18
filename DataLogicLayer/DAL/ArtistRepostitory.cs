using DataLogicLayer.Entitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer.DAL
{
    public class ArtistRepostitory
    {
        DatabaseConnection _dbConnection = new DatabaseConnection();


        public List<Artist> GetAllArtists()
        {
            var artists = new List<Artist>();

            if (_dbConnection.OpenConnection())
            {
                string query = "SELECT `id`, `name` FROM `artists` ORDER BY id DESC";

                using (var command = new MySqlCommand(query, _dbConnection.connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Artist artist = new Artist
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString(),
                            };
                            artists.Add(artist);
                        }
                    }
                }
                _dbConnection.CloseConnection();
            }
            return artists;
        }

        public void CreateNewArtist(Artist artist)
        {
            string query = "INSERT INTO `artists`(`id`, `name`) VALUES ('',@Name)";
            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=musicharp_db;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", artist.Name);
                    cmd.ExecuteNonQuery();
                }
                _dbConnection.CloseConnection();
            }
        }
    }
}
