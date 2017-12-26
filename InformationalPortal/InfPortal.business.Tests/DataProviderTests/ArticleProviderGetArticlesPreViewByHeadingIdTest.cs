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
    public class ArticleProviderGetArticlesPreViewByHeadingIdTest
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
                .Setup(sp => sp.GetArticlesPreViewByHeadingId(It.IsInRange<int>(1,100,Moq.Range.Inclusive)))
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
        [ExpectedException(typeof(ArgumentException))]
        public void ArticleProvider_GetArticlesPreViewByHeadingId_PassNull_Exception()
        {
            articleProvider.GetFullArticleById(null);
        }
        [TestMethod]
        public void ArticleProvider_GetArticlesPreViewByHeadingId_PassNonExistingId_NullIsReturned()
        {
            articleRepository
                .Setup(sp => sp.GetArticlesPreViewByHeadingId(777))
                .Returns(() => { return null; });
            var result = articleProvider.GetFullArticleById(777);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void ArticleProvider_GetArticlesPreViewByHeadingId_PassExistingId_NotNullIsReturned()
        {
            var result = articleProvider.GetArticlesPreViewByHeadingId(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(DataBaseConnectionException))]
        public void ArticleProvider_GetArticlesPreViewByHeadingId_DataBaseConnectionEcxeption_Exception()
        {
            throw new DataBaseConnectionException("Error");
        }

    }
}
