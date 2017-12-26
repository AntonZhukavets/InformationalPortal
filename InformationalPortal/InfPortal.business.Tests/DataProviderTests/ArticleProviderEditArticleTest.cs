using Moq;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfPortal.business.DTO;
using InfPortal.business.Interfaces;
using InfPortal.business.Providers;
using InfPortal.common.Exceptions;
using InfPortal.data.Entities;
using InfPortal.data.Interfaces;


namespace InfPortal.business.Tests.DataProviderTests
{
    [TestClass]
    public class ArticleProviderEditArticleTest
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
               .Setup(sp => sp.EditArticle(It.IsAny<Article>()))
               .Returns(true);
            articleProvider = new ArticleProvider(articleRepository.Object, cacheProvider.Object);
        }
        [TestMethod]
        public void ArticleProvider_EditArticle_PassNull_FalseIsReturned()
        {
            var result = articleProvider.EditArticle(null);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ArticleProvider_EditArticle_PassHeadingsNull_FalseIsReturned()
        {
            var result = articleProvider.EditArticle(new ArticleDTO()
            {
                Headings = null
            });
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ArticleProvider_EditArticle_GetTrueFromServiceProvider_TrueIsReturned()
        {
            var result = articleProvider.EditArticle(new ArticleDTO()
            {
                Id = 1,
                Name = "ArticleName",
                AuthorId = 1,
                AuthorName = "AuthorName",
                PictureLink = "PictureLink",
                Details = new InfoDTO(),
                Headings = new List<HeadingDTO>().ToArray()
            });
            Assert.IsTrue(result);
        }
        [TestMethod]
        [ExpectedException(typeof(DataBaseConnectionException))]
        public void ArticleProvider_EditArticle_DataBaseConnectionEcxeption_Exception()
        {
            throw new DataBaseConnectionException("Error");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArticleProvider_EditArticale_CatchArgumentException()
        {
            articleRepository
                .Setup(sp => sp.EditArticle(It.IsAny<Article>()))
                .Throws<ArgumentException>();
            articleProvider.EditArticle(new ArticleDTO()
            {
                Id = 1,
                Name = "ArticleName",
                AuthorId = 1,
                AuthorName = "AuthorName",
                PictureLink = "PictureLink",
                Details = new InfoDTO(),
                Headings = new List<HeadingDTO>().ToArray()
            });
        }

        [TestMethod]
        public void ArticleProvider_EditArticale_ServiceProviderReturnedTrue_IsCacheRemoved()
        {            
            articleProvider.EditArticle(new ArticleDTO()
            {
                Id = 1,
                Name = "ArticleName",
                AuthorId = 1,
                AuthorName = "AuthorName",
                PictureLink = "PictureLink",
                Details = new InfoDTO(),
                Headings = new List<HeadingDTO>()
                {
                    new HeadingDTO()
                    {
                        Id=1,
                        Name="Travelling",
                        Description="All about travelling"
                    }
                }.ToArray()
            });
            cacheProvider.Verify(cp => cp.Remove(It.IsAny<string>()), Times.AtLeast(3));
        }
    }
}
