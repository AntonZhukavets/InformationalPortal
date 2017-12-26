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
using InfPortal.service.Contracts;
using InfPortal.service.Entities;




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

        private bool ArticleValidator(ArticleEntity article)
        {
            foreach(var item in article.Headings)
            {
                if(string.IsNullOrEmpty(item.Id.ToString()))
                {
                    return false;
                }
            }
            bool resultOfValidation = true;
            if( string.IsNullOrEmpty(article.Name) ||
                string.IsNullOrEmpty(article.PictureLink) ||
                string.IsNullOrEmpty(article.Details.Date.ToString()) ||
                string.IsNullOrEmpty(article.AuthorId.ToString()) ||
                string.IsNullOrEmpty(article.Details.Language.LanguageId.ToString()) ||
                string.IsNullOrEmpty(article.Details.Text))
            {
                resultOfValidation = false;
            }
                
            return resultOfValidation;
        }
        public ArticleEntity[] GetArticlesPreView()
        {
            var articles = new List<ArticleEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetArticlesPreView", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    if(!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }
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

        public ArticleEntity GetFullArticleById(int? id)
        {                      
            if (!id.HasValue)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            var article = new ArticleEntity();
            var headings=new List<HeadingEntity>();
            var articleLinks = new List<ArticleLinkEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetFullArticleById", connection);                    
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("articleId", id);
                    var reader = command.ExecuteReader(); 
                    if(!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }
                    while (reader.Read())
                    {
                        article.Id = Convert.ToInt32(reader["id"]);
                        article.Name = reader["name"].ToString();
                        article.PictureLink = reader["pictureLink"].ToString();
                        article.Details = new InfoEntity()
                        {
                            Id = article.Id,
                            Text = reader["text"].ToString(),
                            Language = new LanguageEntity()
                            {
                                LanguageId= Convert.ToInt32(reader["languageId"]),
                                LanguageName = reader["languageName"].ToString()
                            },
                            Date = Convert.ToDateTime(reader["date"]),
                            VideoLink = reader["videoLink"].ToString()
                        };
                        article.AuthorId = Convert.ToInt32(reader["userId"]);
                        article.AuthorName = reader["login"].ToString();
                    }
                    reader.Close();
                    command = new SqlCommand("GetHeadingsByArticleId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("articleId", id);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        headings.Add(new HeadingEntity()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"].ToString()
                        });
                    }
                    reader.Close();
                    var rnd = new Random(1);                    
                    article.Headings = headings.ToArray<HeadingEntity>();
                    command = new SqlCommand("GetArticlesLinksByHeadingId", connection);                   
                    command.CommandType = CommandType.StoredProcedure;                   
                    foreach(var item in article.Headings)
                    {                        
                        command.Parameters.AddWithValue("headingId", item.Id);                        
                        reader = command.ExecuteReader();
                        if(!reader.HasRows)
                        {
                            continue;
                        }
                        while (reader.Read())
                        {
                            articleLinks.Add(new ArticleLinkEntity()
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString()
                            });
                        }
                        command.Parameters.Clear();                        
                        reader.Close();
                    }
                    article.ArticleLinks = articleLinks.ToArray<ArticleLinkEntity>();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Logger.AddToLog("error", ex.Message);
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorConnection + ex.Message
                    });
                }

            }
            return article;
        }

        public ArticleEntity[] GetArticlesPreViewByHeadingId(int? id)
        {            
            if (!id.HasValue)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            var articles = new List<ArticleEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetArticlesPreViewByHeadingId", connection);
                    command.Parameters.AddWithValue("HeadingId", id);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    if(!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }
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
                    connection.Close();
                    Logger.AddToLog("error", ex.Message);
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorConnection + ex.Message
                    });
                }
            }
            return articles.ToArray<ArticleEntity>();
        }
        
        public ArticleEntity[] GetArticlesPreViewByHeadingIdAndDatePeriod(DateTime dateFrom, DateTime dateTo, int? id)
        {
            if (!id.HasValue || dateFrom==null || dateTo==null)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            var articles = new List<ArticleEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetArticlesPreViewByHeadingIdAndDatePeriod", connection);
                    command.Parameters.AddWithValue("headingId", id);
                    command.Parameters.AddWithValue("@dateFrom", dateFrom);
                    command.Parameters.AddWithValue("@dateTo", dateTo);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }
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
                    connection.Close();
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


        public ArticleEntity[] GetArticlesPreViewByUserId(int? id)
        {            
            if (!id.HasValue)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            var articles = new List<ArticleEntity>();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("GetArticlesPreViewByUserId", connection);
                    command.Parameters.AddWithValue("userId", id);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    if(!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }
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
                    connection.Close();
                    Logger.AddToLog("error", ex.Message);
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorConnection + ex.Message
                    });
                }
            }
            return articles.ToArray<ArticleEntity>();

        }


        public ArticleEntity[] GetArticlesPreViewByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var articles = new List<ArticleEntity>();
                using (var connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        var command = new SqlCommand("GetArticlesPreViewByArticleNameOrText", connection);
                        command.Parameters.AddWithValue("articleName", name);
                        command.CommandType = CommandType.StoredProcedure;
                        var reader = command.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            connection.Close();
                            return null;
                        }
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
                        connection.Close();
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
                        if (!reader.HasRows)
                        {
                            connection.Close();
                            return null;
                        }
                        while (reader.Read())
                        {
                            names.Add(reader["name"].ToString());

                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
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
            if(article==null || !ArticleValidator(article))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));               
            }
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
                    command.Parameters.AddWithValue("languageId", article.Details.Language.LanguageId);
                    command.Parameters.AddWithValue("date", article.Details.Date);
                    command.Parameters.AddWithValue("text", article.Details.Text);
                    command.Parameters.AddWithValue("videoLink", article.Details.VideoLink);
                    lastId = Convert.ToInt32(command.ExecuteScalar());
                    command = new SqlCommand("AddArticlesHeadings", connection);
                    command.CommandType=CommandType.StoredProcedure;
                    if (lastId != 0)
                    {
                        foreach (var item in article.Headings)
                        {
                            command.Parameters.AddWithValue("articleId", lastId);
                            command.Parameters.AddWithValue("headingId", item.Id);
                            command.ExecuteNonQuery();                       
                            command.Parameters.Clear();
                        }
                    }
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
        public bool EditArticle(ArticleEntity article)
        {
            if (article == null || !ArticleValidator(article))
            {
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            bool resultOfOperation = false;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("UpdateArticle", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", article.Id);
                    command.Parameters.AddWithValue("name", article.Name);
                    command.Parameters.AddWithValue("pictureLink", article.PictureLink);                    
                    command.Parameters.AddWithValue("languageId", article.Details.Language.LanguageId);
                    command.Parameters.AddWithValue("date", article.Details.Date);
                    command.Parameters.AddWithValue("text", article.Details.Text);
                    command.Parameters.AddWithValue("videoLink", article.Details.VideoLink);
                    command.ExecuteNonQuery();
                    command = new SqlCommand("DeleteArticlesHeadings", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("articleId", article.Id);
                    command.ExecuteNonQuery();
                    command = new SqlCommand("AddArticlesHeadings", connection);                   
                    command.CommandType = CommandType.StoredProcedure; 
                    foreach (var item in article.Headings) 
                    {                       
                        command.Parameters.AddWithValue("articleId", article.Id);
                        command.Parameters.AddWithValue("headingId", item.Id);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }                    
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


        public bool DeleteArticle(int? id)
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
                    var command = new SqlCommand("DeleteArticlesHeadings", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("articleId", id);
                    command.ExecuteNonQuery();
                    command = new SqlCommand("DeleteArticle", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("articleId", id);                   
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
