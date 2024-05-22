using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Repo_Interfaces;
using BusinessLogicLayer.EntityDTO_s;
using BusinessLogicLayer.Entitys;

namespace DataLogicLayer.DAL
{
    public class UserRepositiry : IUserRepository
    {
        DatabaseConnection _dbConnection = new DatabaseConnection();
        public (bool, int, string, string) Login(string username, string password)
        {
            string queryLogin = "SELECT id, name, profile_picture FROM users WHERE name = @Username AND password = @Password;";
            try
            {
                using (var connection = new MySqlConnection("Server = studmysql01.fhict.local; Uid = dbi538679; Database = dbi538679; Password = Nsp3lEXftR;"))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(queryLogin, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        

                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                int UserId = Convert.ToInt32(dataReader["id"]);
                                string ProfilePicture = dataReader["profile_picture"].ToString();
                                string Name = dataReader["name"].ToString();
                                
                                return (true, UserId, ProfilePicture, Name);
                            }
                            else
                            {
                                return (false, 0, null, null);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                return (false, 0, null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return (false, 0, null, null);
            }
        }

        public void SignUp(UserDTO user)
        {
            string query = "INSERT INTO `users` (`id`, `name`, `email`, `password`, `profile_picture`) VALUES (NULL, @Name, 'test@123.com', @Password, @ProfilePicture)";

            using (var connection = new MySqlConnection("Server = studmysql01.fhict.local; Uid = dbi538679; Database = dbi538679; Password = Nsp3lEXftR;"))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@ProfilePicture", user.ProfilePicture);
                    cmd.ExecuteNonQuery();
                }
                _dbConnection.CloseConnection();
            }
        }
    }
}
