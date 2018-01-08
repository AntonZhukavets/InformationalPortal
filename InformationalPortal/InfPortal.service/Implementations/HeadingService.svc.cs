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
using InfPortal.service.Business.Logs;
using InfPortal.service.Entities;


namespace InfPortal.service.Contracts
{  
    internal class HeadingService : IHeadingService
    {
        private readonly string connectionString = string.Empty;
        const string errorArgument = "Parametr is invalid";
        const string errorConnection = "Something wrong with database. Details:  ";
        private readonly IServiceLoger serviceLoger;
        public HeadingService():this(new ServiceLoger())
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["InfPortal"].ConnectionString;
            }
            catch (Exception ex)
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
                throw new FaultException<ServiceException>(new ServiceException()
                {
                    ErrorMessage = errorConnection + ex.Message
                });
            }
        }
        public HeadingService(IServiceLoger serviceLoger)
        {
            this.serviceLoger = serviceLoger;
        }

        public HeadingEntity[] GetHeadings()
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
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
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;     
            if (!id.HasValue)
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
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
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (heading == null || string.IsNullOrEmpty(heading.Name))
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
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
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (heading == null || string.IsNullOrEmpty(heading.Name))
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
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
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!id.HasValue)
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
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
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
