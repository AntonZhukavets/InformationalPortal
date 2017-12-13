using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InfPortal.service.Entities;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using InfPortal.common.Logs;
using InfPortal.common.Exceptions;
using InfPortal.service.Implementations;

namespace InfPortal.service.Contracts
{  public class HeadingService : IHeadingService
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
            var headings = new List<HeadingEntity>();
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new ArgumentException(errorArgument);
            }
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetHeadingsByArticleId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", parsedId);
                    var reader = command.ExecuteReader();
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
    }
}
