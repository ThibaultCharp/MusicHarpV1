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

        public (bool, int) Login(string username, string password)
        {
            string queryLogin = "SELECT id, name FROM users WHERE name = @Username AND password = @Password;";
            try
            {
                using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=musicharp_db;UID=root;PASSWORD="))
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

                                return (true, UserId);
                            }
                            else
                            {
                                return (false, 0); 
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                return (false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return (false, 0);
            }
        }

        public void SignUp(UserDTO user)
        {
            string query = "INSERT INTO `users` (`id`, `name`, `email`, `password`, `profile_picture`) VALUES (NULL, @Name, 'test@123.com', @Password, @ProfilePicture)";

            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=musicharp_db;UID=root;PASSWORD="))
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
