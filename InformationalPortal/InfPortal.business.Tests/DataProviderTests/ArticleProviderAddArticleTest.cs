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
    public class ArticleProviderAddArticleTest
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
               .Setup(sp => sp.AddArticle(It.IsAny<Article>()))
               .Returns(true);
            articleProvider = new ArticleProvider(articleRepository.Object, cacheProvider.Object);
        }
        [TestMethod]
        public void ArticleProvider_AddArticle_PassNull_FalseIsReturned()
        {            
            var result = articleProvider.AddArticle(null);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ArticleProvider_AddArticle_PassHeadingsNull_FalseIsReturned()
        {
            var result = articleProvider.AddArticle(new ArticleDTO()
            {
                Headings=null
            });
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ArticleProvider_AddArticle_GetTrueFromServiceProvider_TrueIsReturned()
        {
            var result = articleProvider.AddArticle(new ArticleDTO()
            {
                Id=1,
                Name="ArticleName",
                AuthorId=1,
                AuthorName="AuthorName",
                PictureLink="PictureLink",
                Details=new InfoDTO(),                
                Headings = new List<HeadingDTO>().ToArray()
            });            
            Assert.IsTrue(result);
        }
        [TestMethod]
        [ExpectedException(typeof(DataBaseConnectionException))]
        public void ArticleProvider_GetFullArticleById_DataBaseConnectionEcxeption_Exception()
        {
            throw new DataBaseConnectionException("Error");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArticleProvider_AddArticale_CatchArgumentException()
        {
            articleRepository
                .Setup(sp => sp.AddArticle(It.IsAny<Article>()))
                .Throws<ArgumentException>();
            articleProvider.AddArticle(new ArticleDTO()
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
        public void ArticleProvider_AddArticale_ServiceProviderReturnedTrue_IsCacheRemoved()
        {            
            articleProvider.AddArticle(new ArticleDTO()
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
            cacheProvider.Verify(cp => cp.Remove(It.IsAny<string>()), Times.AtLeast(2));
        }
    }
    
}
