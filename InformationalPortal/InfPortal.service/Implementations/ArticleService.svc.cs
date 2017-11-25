using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InfPortal.service.DBEntities;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

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
            var connection = new SqlConnection(connectionString);
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
                    PictureLink = reader["pictureLink"].ToString() ,
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

        public ArticleEntity GetArticleById(int? id)
        {
            ArticleEntity article = new ArticleEntity();           
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("GetArticleById", connection);
            command.CommandType = CommandType.StoredProcedure;
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                article.Id=Convert.ToInt32(reader["id"]);
                article.Name = reader["name"].ToString();
                article.PictureLink = reader["pictureLink"].ToString();
                article.Details.Id = Convert.ToInt32(reader["id"]);
                article.Details.Text = reader["text"].ToString();
                article.Details.Language = reader["language"].ToString();
                article.Details.Date = Convert.ToDateTime(reader["date"]);
                article.Details.VideoLink = reader["videoLink"].ToString();
                
            }
            connection.Close();
            return article;            
        }

        public List<ArticleEntity> GetArticlesByHeadingName(string HeadingName)
        {
            List<ArticleEntity> articles = new List<ArticleEntity>();
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("GetArticlesByHeadingName", connection);
            command.Parameters.Add("HeadingName", HeadingName);
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
