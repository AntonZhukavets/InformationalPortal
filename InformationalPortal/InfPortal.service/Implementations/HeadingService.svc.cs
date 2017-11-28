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
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "HeadingService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы HeadingService.svc или HeadingService.svc.cs в обозревателе решений и начните отладку.
    public class HeadingService : IHeadingService
    {
        string connectionString = string.Empty;

        public HeadingService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["InfPortal"].ConnectionString;
        }

        public List<HeadingEntity> GetHeadings()
        {
            List<HeadingEntity> headings = new List<HeadingEntity>();
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
                        ErrorMessage = "Can't connect to DataBase..."
                    });
                }
            }
            return headings;
        }

        public List<HeadingEntity> GetHeadingsByArticleId(int? id)
        {
            List<HeadingEntity> headings = new List<HeadingEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetHeadingsByArticleId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", id);
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
                        ErrorMessage = "Can't connect to DataBase..."
                    });
                }
            }
            return headings;
        }
    }
}
