using BusinessLogicLayer.EntityDTO_s;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Repo_Interfaces;

namespace DataLogicLayer.DAL
{
    public class ArtistRepostitory : IArtistRepository
    {
        DatabaseConnection _dbConnection = new DatabaseConnection();


        public List<ArtistDTO> GetAllArtists()
        {
            var artists = new List<ArtistDTO>();

            if (_dbConnection.OpenConnection())
            {
                string query = "SELECT `id`, `name` FROM `artists` ORDER BY id DESC";

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
                _dbConnection.CloseConnection();
            }
            return artists;
        }

        public void CreateNewArtist(ArtistDTO artist)
        {
            if (_dbConnection.OpenConnection())
            {
                string query = "INSERT INTO `artists`(`id`, `name`) VALUES ('',@Name)";
                using (var command = new MySqlCommand(query, _dbConnection.connection))
                {
                    command.Parameters.AddWithValue("@Name", artist.Name);
                    command.ExecuteNonQuery();
                }
                _dbConnection.CloseConnection();

            }
        }
    }
}
