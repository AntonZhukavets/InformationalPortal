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
using InfPortal.service.Business.Logs;

namespace InfPortal.service.Contracts
{
    public class LanguageService : ILanguageService
    {
        private readonly string connectionString = string.Empty;
        private const string errorArgument = "Parametr is invalid";
        private const string errorConnection = "Something wrong with database. Details:  ";
        private readonly IServiceLoger serviceLoger;
        public LanguageService():this(new ServiceLoger())
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
        public LanguageService(IServiceLoger serviceLoger)
        {
            this.serviceLoger = serviceLoger;
        }

        public LanguageEntity[] GetLanguages()
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
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
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (language == null || string.IsNullOrEmpty(language.LanguageName))
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
        public bool RestoreLanguage(int? id)
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
                    var command = new SqlCommand("RestoreLanguage", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("languageId", id);
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

        public bool DeleteLanguage(int? id)
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
                    var command = new SqlCommand("DeleteLanguage", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("languageId", id);
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
