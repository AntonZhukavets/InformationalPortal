using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using InfPortal.service.Contracts;
using InfPortal.service.Entities;
using InfPortal.common.Logs;
using InfPortal.common.Exceptions;
namespace InfPortal.service.Contracts
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ArticleService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы ArticleService.svc или ArticleService.svc.cs в обозревателе решений и начните отладку.
    public class ArticleService : IArticleService
    {
        string connectionString = string.Empty;

        public ArticleService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["InfPortal"].ConnectionString;
        }
        public List<ArticleEntity> GetArticles()
        {
            List<ArticleEntity> articles = new List<ArticleEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetArticles", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        articles.Add(new ArticleEntity()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            PictureLink = reader["pictureLink"].ToString(),
                            Details = new InfoEntity()
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Text = reader["text"].ToString(),
                                Date = Convert.ToDateTime(reader["date"]),
                                Language = reader["language"].ToString(),
                                VideoLink = reader["videoLink"].ToString()
                            }

                        });
                    }
                    connection.Close();
                }
                catch(Exception ex)
                {
                    Logger.AddToLog("error", ex.Message);                    
                }
            }
            return articles;
        }

        public ArticleEntity GetArticleById(int? id)
        {
           if(id==null)
           {
               throw new ArgumentNullException("id is null");
               
           }
            ArticleEntity article = new ArticleEntity();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetArticleById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("articleId", id);                    
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        article.Id = Convert.ToInt32(reader["id"]);
                        article.Name = reader["name"].ToString();
                        article.PictureLink = reader["pictureLink"].ToString();
                        article.Details = new InfoEntity()
                        {
                            Id = article.Id,
                            Text = reader["text"].ToString(),
                            Language = reader["language"].ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            VideoLink = reader["videoLink"].ToString()
                        };                        

                    }
                    connection.Close();
                }
                catch(Exception ex)
                {                    
                    Logger.AddToLog("error", ex.Message);                    
                }

            }
            return article;            
        }

        public List<ArticleEntity> GetArticlesByHeadingName(string HeadingName)
        {
            List<ArticleEntity> articles = new List<ArticleEntity>();
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("GetArticlesByHeadingName", connection);
            command.Parameters.AddWithValue("HeadingName", HeadingName);
            command.CommandType = CommandType.StoredProcedure;
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                articles.Add(new ArticleEntity()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"].ToString(),
                    PictureLink = reader["pictureLink"].ToString(),
                    Details = new InfoEntity()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Text = reader["text"].ToString(),
                        Date = Convert.ToDateTime(reader["date"]),
                        Language = reader["language"].ToString(),
                        VideoLink = reader["videoLink"].ToString()
                    }

                });
            }
            connection.Close();
            return articles;
        }

        public int GetCountOfArticles()
        {
            throw new NotImplementedException();
        }
    }
}
