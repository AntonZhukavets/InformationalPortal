using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InfPortal.service.Business.Exceptions;
using InfPortal.service.Entities;

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
        
        private bool UserValidator(UserEntity user)
        {
            bool resultOfValidation = true;
            if( string.IsNullOrEmpty(user.FirstName) ||
                string.IsNullOrEmpty(user.LastName) ||
                string.IsNullOrEmpty(user.Email) ||                
                string.IsNullOrEmpty(user.Login) ||
                string.IsNullOrEmpty(user.Password))                
            {
                resultOfValidation = false;
            }
            return resultOfValidation;
        }
        public bool Register(UserEntity user)
        {
            if (user == null || !UserValidator(user))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));               
            }
            bool resultOfRegistration = false;
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
                    var result = Convert.ToInt32(command.ExecuteScalar());
                    if(result!=0)
                    {
                        resultOfRegistration = true;
                    }
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
                return resultOfRegistration;
            }
        }


        public bool IsValidUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
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
                    if (searchResult == 1)
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


        public UserEntity GetUserByLoginAndPassword(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }            
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetUserByLoginAndPassword", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("login", login);
                    command.Parameters.AddWithValue("password", password);
                    var reader = command.ExecuteReader();
                    if(!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }
                    var user = new UserEntity();
                    while(reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["userId"]);
                        user.FirstName = reader["firstName"].ToString();
                        user.LastName = reader["lastName"].ToString();
                        user.Email = reader["email"].ToString();
                        user.Login = reader["login"].ToString();                        
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
                    return user;
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

        public bool UpdateUser(UserEntity user)
        {
            if (user == null || !UserValidator(user))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            bool resultOfUpdating = false;
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
                    if(command.ExecuteNonQuery()!=0)
                    {
                        resultOfUpdating = true;
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
            return resultOfUpdating;
        }


        public UserEntity GetUserById(int? id)
        {       
            if (!id.HasValue)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            var user = new UserEntity();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetUserById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("userId", id);
                    var reader = command.ExecuteReader();
                    if(!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }
                    while (reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["userId"]);
                        user.FirstName = reader["firstName"].ToString();
                        user.LastName = reader["lastName"].ToString();
                        user.Email = reader["email"].ToString();
                        user.Login = reader["login"].ToString();                       
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
                        user.IsBlocked = Convert.ToBoolean(Convert.ToInt32(reader["isBlocked"]));

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

        public bool DeleteUser(int? id)
        {                       
            if (!id.HasValue)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            bool result = false; 
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("DeleteUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("userId", id);
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


        public bool ResumeUser(int? id)
        {  
            if (!id.HasValue)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            bool result = false;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("ResumeUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("userId", id);
                    if (command.ExecuteNonQuery() != 0)
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


        public bool MakeAdmin(int? id)
        {          
            if (!id.HasValue)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            bool result = false;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("MakeAdmin", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("userId", id);
                    if (command.ExecuteNonQuery() != 0)
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


        public UserEntity[] GetAllUsers()
        {
            var users = new List<UserEntity>();         
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetAllUsers", connection);
                    command.CommandType = CommandType.StoredProcedure;                    
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }                    
                    while (reader.Read())
                    {
                        users.Add(new UserEntity()
                        {
                            Id = Convert.ToInt32(reader["userId"]),
                            FirstName = reader["firstName"].ToString(),
                            LastName = reader["lastName"].ToString(),
                            Email = reader["email"].ToString(),
                            Login = reader["login"].ToString(),
                            Password = reader["password"].ToString(),
                            Role = new RoleEntity()
                            {
                                Id = Convert.ToInt32(reader["roleId"]),
                                Name = reader["roleName"].ToString(),
                                Description = reader["roleDesc"].ToString()
                            },
                            IsBlocked = Convert.ToBoolean(reader["isBlocked"])
                        });                        

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
            return users.ToArray<UserEntity>();
        }
    }
}
