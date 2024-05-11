﻿using AuthoWorkAppp.Model;
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

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAll()
        {
            IEnumerable<UserModel> userModels = new ObservableCollection<UserModel>();
            using(var connection = GetConnection())
            using(var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User]";
                
                using(var reader =  command.ExecuteReader())
                {
                    while(reader.Read()) 
                    {
                        userModels.Append(new UserModel
                        {
                            Id = reader["Id"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Name = reader["Name"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["email"].ToString(),
                        });
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
            throw new NotImplementedException();
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
