using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InfPortal.common.Logs;
using InfPortal.common.Exceptions;
using InfPortal.service.Business.Exceptions;
using InfPortal.service.Entities;


namespace InfPortal.service.Contracts
{  
    internal class HeadingService : IHeadingService
    {
        string connectionString = string.Empty;

        const string errorArgument = "Parametr is invalid";
        const string errorConnection = "Something wrong with database. Details:  ";
        public HeadingService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["InfPortal"].ConnectionString;
        }

        public HeadingEntity[] GetHeadings()
        {
           var headings = new List<HeadingEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetAllHeadings", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    if(!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }
                    while (reader.Read())
                    {
                        headings.Add(new HeadingEntity()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"].ToString()

                        });
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Logger.AddToLog("error", ex.Message);
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorConnection + ex.Message
                    });
                }
            }
            return headings.ToArray<HeadingEntity>();
        }

        public HeadingEntity[] GetHeadingsByArticleId(int? id)
        {                      
            if (!id.HasValue)
            {
                throw new ArgumentException(errorArgument);
            }
            var headings = new List<HeadingEntity>(); 
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetHeadingsByArticleId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", id);
                    var reader = command.ExecuteReader();
                    if(!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }
                    while (reader.Read())
                    {
                        headings.Add(new HeadingEntity()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"].ToString()

                        });
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Logger.AddToLog("error", ex.Message);
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorConnection+ex.Message
                    });
                }
            }
            return headings.ToArray<HeadingEntity>();
        }


        public bool AddHeading(HeadingEntity heading)
        {
            if (heading == null || string.IsNullOrEmpty(heading.Name))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            bool resultOfOperation = false;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("AddHeading", connection);
                    command.CommandType = CommandType.StoredProcedure;                    
                    command.Parameters.AddWithValue("name", heading.Name);
                    command.Parameters.AddWithValue("desc", heading.Description);
                    command.ExecuteNonQuery();
                    connection.Close();
                    resultOfOperation = true;
                }
                catch (Exception ex)
                {
                    //Logger.AddToLog("error", ex.Message);
                    connection.Close();
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorConnection + ex.Message
                    });
                }
                return resultOfOperation;
            }
        }

        public bool EditHeading(HeadingEntity heading)
        {
            if (heading == null || string.IsNullOrEmpty(heading.Name))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            bool resultOfOperation = false;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("UpdateHeading", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", heading.Id);
                    command.Parameters.AddWithValue("name", heading.Name);
                    command.Parameters.AddWithValue("desc", heading.Description);               
                    command.ExecuteNonQuery();                   
                    connection.Close();
                    resultOfOperation = true;
                }
                catch (Exception ex)
                {
                    //Logger.AddToLog("error", ex.Message);
                    connection.Close();
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorConnection + ex.Message
                    });
                }
                return resultOfOperation;
            }
        }

        public bool DeleteHeading(int? id)
        {
            if (!id.HasValue)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            bool resultOfOperation = false;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("DeleteHeading", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", id);                    
                    command.ExecuteNonQuery();
                    connection.Close();
                    resultOfOperation = true;
                }
                catch (Exception ex)
                {
                    //Logger.AddToLog("error", ex.Message);
                    connection.Close();
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorConnection + ex.Message
                    });
                }
                return resultOfOperation;
            }
        }
    }
}
