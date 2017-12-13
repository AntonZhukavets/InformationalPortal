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
using InfPortal.service.Implementations;
namespace InfPortal.service.Contracts
{
    public class ArticleService : IArticleService
    {
        string connectionString = string.Empty;
        const string errorConnection = "Something wrong with database. Details:  ";
        const string errorArgument = "Parametr is invalid";

        public ArticleService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["InfPortal"].ConnectionString;
        }
        public ArticleEntity[] GetArticles()
        {
            var articles = new List<ArticleEntity>();
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
                            },
                            AuthorId = Convert.ToInt32(reader["userId"]),
                            AuthorName = reader["login"].ToString()
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
            return articles.ToArray<ArticleEntity>();
        }

        public ArticleEntity GetArticleById(int? id)
        {
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            var article = new ArticleEntity();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetArticleById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("articleId", parsedId);
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
                        article.AuthorId = Convert.ToInt32(reader["userId"]);
                        article.AuthorName = reader["login"].ToString();
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
            return article;
        }

        public ArticleEntity[] GetArticlesByHeadingId(int? id)
        {
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            var articles = new List<ArticleEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetArticlesByHeadingId", connection);
                    command.Parameters.AddWithValue("HeadingId", parsedId);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        articles.Add(new ArticleEntity()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            PictureLink = reader["pictureLink"].ToString(),
                            AuthorId = Convert.ToInt32(reader["userId"]),
                            AuthorName = reader["login"].ToString()

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
            return articles.ToArray<ArticleEntity>();
        }

        public int GetCountOfArticles()
        {
            throw new NotImplementedException();
        }


        public ArticleEntity[] GetArticlesByUserId(int? id)
        {
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            var articles = new List<ArticleEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetArticlesByUserId", connection);
                    command.Parameters.AddWithValue("userId", parsedId);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        articles.Add(new ArticleEntity()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            PictureLink = reader["pictureLink"].ToString(),
                            AuthorId = Convert.ToInt32(reader["userId"]),
                            AuthorName = reader["login"].ToString()
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
            return articles.ToArray<ArticleEntity>();

        }


        public ArticleEntity[] GetArticleByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var articles = new List<ArticleEntity>();
                using (var connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        var command = new SqlCommand("GetArticleByName", connection);
                        command.Parameters.AddWithValue("articleName", name);
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
                                },
                                AuthorId = Convert.ToInt32(reader["userId"]),
                                AuthorName = reader["login"].ToString()
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
                    return articles.ToArray<ArticleEntity>();
                }
            }
            return null;
        }

        public string[] GetArticleNamesByInput(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var names = new List<string>();
                using (var connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        var command = new SqlCommand("GetArticleNamesByInput", connection);
                        command.Parameters.AddWithValue("articleName", name);
                        command.CommandType = CommandType.StoredProcedure;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            names.Add(reader["name"].ToString());

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
                    return names.ToArray();
                }
            }
            return null;
        }


        public bool AddArticle(ArticleEntity article)
        {
            bool resultOfOperation = false;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    int lastId = 0;
                    connection.Open();
                    var command = new SqlCommand("AddArticle", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("name", article.Name);
                    command.Parameters.AddWithValue("pictureLink", article.PictureLink);
                    command.Parameters.AddWithValue("userId", article.AuthorId);
                    command.Parameters.AddWithValue("language", article.Details.Language);
                    command.Parameters.AddWithValue("date", article.Details.Date);
                    command.Parameters.AddWithValue("text", article.Details.Text);
                    command.Parameters.AddWithValue("videoLink", article.Details.VideoLink);
                    lastId = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    if (lastId != 0)
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
    }
}
