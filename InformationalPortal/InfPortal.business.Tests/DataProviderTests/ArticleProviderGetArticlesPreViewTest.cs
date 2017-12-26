using Moq;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfPortal.business.Interfaces;
using InfPortal.business.Providers;
using InfPortal.common.Exceptions;
using InfPortal.data.Entities;
using InfPortal.data.Interfaces;


namespace InfPortal.business.Tests.DataProviderTests
{
    [TestClass]
    public class ArticleProviderGetArticlesPreViewTest
    {
        private IArticleProvider articleProvider;
        private Mock<IArticleRepository> articleRepository;
        private Mock<ICacheProvider> cacheProvider;
        [TestInitialize]
        public void TestInitialize()
        {
            articleRepository = new Mock<IArticleRepository>();
            cacheProvider = new Mock<ICacheProvider>();
            articleRepository
                .Setup(sp => sp.GetArticlesPreView())
                .Returns(new List<Article>()
                {
                    new Article()
                    {
                        Id = 1,
                        Name = "ArticleName",
                        PictureLink = "PictureLink",
                        AuthorId = 1,
                        AuthorName = "AuthorName",
                        Details = new Info()
                        {
                            Id = 1,
                            Date = DateTime.Now,
                            Text = "SomeText",
                            VideoLink = "SomeVideoLink",
                            Language = new Language()
                            {
                                LanguageId= 1,
                                LanguageName="English"
                            }
                        },
                        Headings = new List<Heading>() 
                        {
                            new Heading()
                            {
                                Id=1,
                                Name="HeadingName",
                                Description="HeadingDescription"
                            }
                        }.ToArray()
                    }
                }.ToArray());
            articleProvider = new ArticleProvider(articleRepository.Object, cacheProvider.Object);
        }
        [TestMethod]        
        public void ArticleProvider_GetArticlesPreView_GetNullFromServiceProvider_NullIsReturned()
        {
            articleRepository.Setup(sp => sp.GetArticlesPreView()).Returns(() => { return null; });            
            var result=articleProvider.GetArticlesPreView();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ArticleProvider_GetArticlesPreView_GetNotNullFromServiceProvider_NotNullIsReturned()
        {       
            var result = articleProvider.GetArticlesPreView();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        [ExpectedException(typeof(DataBaseConnectionException))]
        public void ArticleProvider__GetArticlesPreView_DataBaseConnectionEcxeption_Exception()
        {
            throw new DataBaseConnectionException("Error");
        }
    }
}
