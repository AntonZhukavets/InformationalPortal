using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InfPortal.service.Entities;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using InfPortal.service.Implementations;

namespace InfPortal.service.Contracts
{
     public class UserService : IUserService
    {
        const string errorArgument = "Parametr is invalid";
        string connectionString = string.Empty;
        private const string errorMessage = "Something wrong with database. Details: ";
        public UserService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["InfPortal"].ConnectionString;
        }
        public void Register(UserEntity user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("RegisterUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("firstName", user.FirstName); 
                    command.Parameters.AddWithValue("lastName", user.LastName);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("login", user.Login);
                    command.Parameters.AddWithValue("password", user.Password);
                    command.Parameters.AddWithValue("age", user.Age);
                    command.ExecuteNonQuery();                   
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //Logger.AddToLog("error", ex.Message);
                    connection.Close();
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorMessage+ex.Message
                    });
                }
            }
        }


        public bool IsValidUser(string login, string password)
        {
            int searchResult = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("Login", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("login", login);
                    command.Parameters.AddWithValue("password", password);
                    searchResult = Convert.ToInt32(command.ExecuteScalar()); 
                    connection.Close();
                    if(searchResult==1)
                    {
                        return true;
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //Logger.AddToLog("error", ex.Message);
                    connection.Close();
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorMessage + ex.Message
                    });                    
                }                
            }
            return false;
        }


        public UserEntity GetUserByLogin(string login)
        {
            var user = new UserEntity();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetUserByLogin", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("login", login);
                    var reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["userId"]);
                        user.FirstName = reader["firstName"].ToString();
                        user.LastName = reader["lastName"].ToString();
                        user.Email = reader["email"].ToString();
                        user.Login = reader["login"].ToString();
                        user.Age = Convert.ToInt32(reader["age"]);
                        user.Password = reader["password"].ToString();
                        int id = Convert.ToInt32(reader["roleId"]);
                        string name = reader["roleName"].ToString();
                        string desc=reader["roleDesc"].ToString();
                        user.Role = new RoleEntity()
                        {
                            Id = id,
                            Name = name,
                            Description=desc
                        };
                        
                    }
                    connection.Close();
                    
                }
                catch (Exception ex)
                {
                    //Logger.AddToLog("error", ex.Message);
                    connection.Close();
                    throw new FaultException<ServiceException>(new ServiceException()
                    {                        
                        ErrorMessage = errorMessage + ex.Message
                    });
                }
            }
            return user;
        }


        public int GetUserIdByLogin(string login)
        {
            var user = new UserEntity();
            int id = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetUserIdByLogin", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("login", login);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader["userId"]);
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    //Logger.AddToLog("error", ex.Message);
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorMessage + ex.Message
                    });
                }
            }
            return id;
        }

        public void UpdateUser(UserEntity user)
        {
           
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("UpdateUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", user.Id);
                    command.Parameters.AddWithValue("firstName", user.FirstName);
                    command.Parameters.AddWithValue("lastName", user.LastName);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("login", user.Login);
                    command.Parameters.AddWithValue("password", user.Password);
                    command.Parameters.AddWithValue("age", user.Age);
                    command.ExecuteNonQuery();                 
                    connection.Close();

                }
                catch (Exception ex)
                {
                    //Logger.AddToLog("error", ex.Message);
                    connection.Close();
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorMessage + ex.Message
                    });
                }
            }
        }


        public UserEntity GetUserById(int? id)
        {
            var user = new UserEntity();
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetUserById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("userId", parsedId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["userId"]);
                        user.FirstName = reader["firstName"].ToString();
                        user.LastName = reader["lastName"].ToString();
                        user.Email = reader["email"].ToString();
                        user.Login = reader["login"].ToString();
                        user.Age = Convert.ToInt32(reader["age"]);
                        user.Password = reader["password"].ToString();
                        int roleId = Convert.ToInt32(reader["roleId"]);
                        string name = reader["roleName"].ToString();
                        string desc = reader["roleDesc"].ToString();
                        user.Role = new RoleEntity()
                        {
                            Id = roleId,
                            Name = name,
                            Description = desc
                        };

                    }
                    connection.Close();

                }
                catch (Exception ex)
                {
                    //Logger.AddToLog("error", ex.Message);
                    connection.Close();
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorMessage + ex.Message
                    });
                }
            }
            return user;
        }

        public bool DeleteUserById(int? id)
        {
            bool result = false;
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("DeleteUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("userId", parsedId);
                    if(command.ExecuteNonQuery()!=0)
                    {
                        result = true;
                    }
                    connection.Close();

                }
                catch (Exception ex)
                {
                    //Logger.AddToLog("error", ex.Message);
                    connection.Close();
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorMessage + ex.Message
                    });
                }
            }
            return result;
        }
    }
}
