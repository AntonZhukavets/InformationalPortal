using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InfPortal.service.Business.Exceptions;
using InfPortal.service.Business.Logs;
using InfPortal.service.Contracts;
using InfPortal.service.Entities;


namespace InfPortal.service.Contracts
{
    public class ArticleService : IArticleService
    {
        private readonly string connectionString = string.Empty;
        private const string errorConnection = "Something wrong with database. Details:  ";
        private const string fileNotFound = "Something wrong with file storage. File not found. Details:  ";
        private string storageNotFound = string.Format("File storage not found. {0} is unreachable ", pathToStorage);
        private const string errorArgument = "Parametr is invalid";
        private const string pathToStorage = @"d:\install\projects8\InformationalPortal\InfPortal.service\Pictures\";
        private const string pathToImage = pathToStorage + "\\{0}.jpg";
        private readonly IServiceLoger serviceLoger;

        public ArticleService():this(new ServiceLoger())
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

        public ArticleService(IServiceLoger serviceLoger)
        {
            this.serviceLoger = serviceLoger;
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

        private byte[] GetPictureFromStore(string fileName)
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string path=string.Format(pathToImage, fileName);
            string defaultPath = string.Format(pathToImage, "default");
            Image image;
            byte[] picture = null;
            try
            {
                if (!Directory.Exists(pathToStorage))
                {
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, storageNotFound ));
                    return picture;
                }
                if (!File.Exists(path))
                {
                    if(!File.Exists(defaultPath))
                    {
                        return picture;
                    }
                    path = defaultPath;
                }
                using (var imageTmp = new Bitmap(path))
                {
                    image = new Bitmap(imageTmp);
                    MemoryStream memoryStream = new MemoryStream();
                    image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg); 
                    picture = memoryStream.ToArray();
                }                
            }
            catch(Exception ex)
            {                
                throw new Exception(ex.Message);
            }           
            return picture;
        }

        public ArticleEntity[] GetArticlesPreView()
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
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
                        var picture = GetPictureFromStore(reader["id"].ToString());
                        articles.Add(new ArticleEntity()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            PictureLink = reader["pictureLink"].ToString(),                            
                            AuthorId = Convert.ToInt32(reader["userId"]),
                            AuthorName = reader["login"].ToString(),
                            Picture=picture
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
            return articles.ToArray<ArticleEntity>();
        }

        public ArticleEntity GetFullArticleById(int? id)
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;   
            if (!id.HasValue)
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
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
                        var picture = GetPictureFromStore(reader["id"].ToString());                     
                        article.Id = Convert.ToInt32(reader["id"]);
                        article.Name = reader["name"].ToString();
                        article.PictureLink = reader["pictureLink"].ToString();
                        article.Picture = picture;
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
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!id.HasValue)
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
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
                        var picture = GetPictureFromStore(reader["id"].ToString());
                        articles.Add(new ArticleEntity()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            PictureLink = reader["pictureLink"].ToString(),
                            AuthorId = Convert.ToInt32(reader["userId"]),
                            AuthorName = reader["login"].ToString(),
                            Picture=picture
                        });
                    }
                    connection.Close();
                }                
                catch (Exception ex)
                {
                    connection.Close();
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!id.HasValue || dateFrom==null || dateTo==null)
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
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
                        var picture = GetPictureFromStore(reader["id"].ToString());
                        articles.Add(new ArticleEntity()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            PictureLink = reader["pictureLink"].ToString(),
                            AuthorId = Convert.ToInt32(reader["userId"]),
                            AuthorName = reader["login"].ToString(),
                            Picture=picture
                        });
                    }
                    connection.Close();
                }                
                catch (Exception ex)
                {
                    connection.Close();
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = errorConnection + ex.Message
                    });
                }
            }
            return articles.ToArray<ArticleEntity>();
        }
        
        public ArticleEntity[] GetArticlesPreViewByUserId(int? id)
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!id.HasValue)
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
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
                        var picture = GetPictureFromStore(reader["id"].ToString());
                        articles.Add(new ArticleEntity()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            PictureLink = reader["pictureLink"].ToString(),
                            AuthorId = Convert.ToInt32(reader["userId"]),
                            AuthorName = reader["login"].ToString(),
                            Picture=picture
                        });
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
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
                            var picture = GetPictureFromStore(reader["id"].ToString());
                            articles.Add(new ArticleEntity()
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString(),
                                PictureLink = reader["pictureLink"].ToString(),                               
                                AuthorId = Convert.ToInt32(reader["userId"]),
                                AuthorName = reader["login"].ToString(),
                                Picture=picture
                            });
                        }
                        connection.Close();
                    }                    
                    catch (Exception ex)
                    {
                        connection.Close();
                        serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
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
                        serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if(article==null || !ArticleValidator(article))
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));               
            }
            if (!Directory.Exists(pathToStorage))
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, storageNotFound));
                return resultOfOperation;
            }            
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    int lastId = 0;
                    int articleId=0;
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
                    articleId=lastId;
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
                        serviceLoger.Info(string.Format("Successfully added new article with id {0}", articleId.ToString()));
                        byte[] picture = article.Picture;
                        MemoryStream memoryStream = new MemoryStream(picture);
                        Image image = Image.FromStream(memoryStream);
                        string fileName = articleId.ToString();
                        image.Save(string.Format(pathToImage, fileName), System.Drawing.Imaging.ImageFormat.Jpeg);                 
                        resultOfOperation = true;
                    }
                }
                catch (IOException ex)
                {
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = ex.Message
                    });
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

        public bool EditArticle(ArticleEntity article)
        {

            bool resultOfOperation = false;
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (article == null || !ArticleValidator(article))
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            if (!Directory.Exists(pathToStorage))
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, storageNotFound));
                return resultOfOperation;
            }  
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
                    if(article.Picture!=null)
                    {
                        byte[] picture = article.Picture;
                        MemoryStream memoryStream = new MemoryStream(picture);
                        Image image = Image.FromStream(memoryStream);
                        string fileName = article.Id.ToString();
                        string filePath = string.Format(pathToImage, fileName);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg); 
                    }
                    serviceLoger.Info(string.Format("Successfully updated article with id {0}", article.Id.ToString()));
                    resultOfOperation = true;                    
                }
                catch (IOException ex)
                {                    
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = ex.Message
                    });
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

        public bool DeleteArticle(int? id)
        {
            bool resultOfOperation = false;
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!id.HasValue)
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, errorArgument));
                throw new FaultException<ArgumentException>(new ArgumentException(errorArgument));
            }
            if (!Directory.Exists(pathToStorage))
            {
                serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, storageNotFound));
                return resultOfOperation;
            }            
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
                    string fileName = id.ToString();
                    string filePath = string.Format(pathToImage, fileName);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    serviceLoger.Info(string.Format("Successfully deleted article with id {0}", id.ToString()));
                    resultOfOperation = true;
                }
                catch (IOException ex)
                {
                    serviceLoger.Error(string.Format(StringsToServiceLoger.exceptionToServiceLogger, currentMethodName, ex.Message));
                    throw new FaultException<ServiceException>(new ServiceException()
                    {
                        ErrorMessage = ex.Message
                    });
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
