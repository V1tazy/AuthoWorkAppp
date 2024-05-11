using AuthoWorkAppp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace AuthoWorkAppp.Repositories
{
    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Insert [User](Username, Password, Name, Email, LastName) " +
                    "Values(@username, @password, @name, @email, @LastName)";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = userModel.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar).Value = userModel.LastName;
                command.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = userModel.Email;

                command.ExecuteScalar();

                MessageBox.Show("Успешно");
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User] where username = @username and password = @password";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;

            }
            return validUser;
        }

        public bool CheckAdminUser(string username)
        {
            if (username == "admin")
                return true;
            else
                return false;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAll()
        {
            var userModels = new List<UserModel>(); 

            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT * FROM [User]", connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var userModel = new UserModel
                        {
                            Id = reader["Id"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Name = reader["Name"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                        };
                        userModels.Add(userModel); 
                    }
                }
            }
            return userModels;
        }


        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;

            using(var connection = GetConnection())
            using(var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User] where username = @username";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = username;

                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
            }
            return user;
        }

        public bool GetExistUserByEmail(string email)
        {
            bool isUserExist;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User] where email = @email";
                command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar).Value = email;

                isUserExist = command.ExecuteScalar() == null ? true : false;
            }

            return isUserExist;
        }

        public bool GetExistUserByName(string username)
        {
            bool isUserExist;
            using(var connection = GetConnection())
            using(var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User] where username = @username";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = username;

                isUserExist = command.ExecuteScalar() == null ? true : false;
            }
            return isUserExist;
        }

        public void Remove(int id)
        {
        }
    }
}
