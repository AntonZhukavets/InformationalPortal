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
    public class LanguageService : ILanguageService
    {
        string connectionString = string.Empty;

        const string errorArgument = "Parametr is invalid";
        const string errorConnection = "Something wrong with database. Details:  ";
        public LanguageService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["InfPortal"].ConnectionString;
        }

        public LanguageEntity[] GetLanguages()
        {
            var languages = new List<LanguageEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetAllLanguages", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }
                    while (reader.Read())
                    {
                        languages.Add(new LanguageEntity()
                        {
                            LanguageId = Convert.ToInt32(reader["languageId"]),
                            LanguageName = reader["languageName"].ToString()
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
            return languages.ToArray<LanguageEntity>();
        }

        public bool AddLanguage(LanguageEntity language)
        {
            if (language == null || string.IsNullOrEmpty(language.LanguageName))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            bool resultOfOperation = false;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("AddLanguage", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("languageName", language.LanguageName);
                    var result = command.ExecuteScalar();
                    connection.Close();
                    if (Convert.ToInt32(result) != 0)
                    {
                        resultOfOperation = true;
                    }
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

        public bool RestoreLanguage(int? id)
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
                    var command = new SqlCommand("RestoreLanguage", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("languageId", id);
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

        public bool DeleteLanguage(int? id)
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
                    var command = new SqlCommand("DeleteLanguage", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("languageId", id);
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
