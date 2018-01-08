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


namespace InfPortal.business.Tests
{
    [TestClass]
    public class ArticleProviderGetFullArticleByIdTest
    {
        private IArticleProvider articleProvider;
        private Mock<IArticleRepository> articleRepository;
        private Mock<ICacheProvider> cacheProvider;
        const string cacheKeyGetFullArticleById = "GetFullArticleById";

        [TestInitialize]
        public void TestInitialize()
        {
            articleRepository = new Mock<IArticleRepository>();
            cacheProvider = new Mock<ICacheProvider>();
            articleRepository
               .Setup(sp => sp.GetFullArticleById(It.IsInRange<int>(1, 100, Moq.Range.Inclusive)))
               .Returns(new Article()
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
                           LanguageId = 1,
                           LanguageName = "English"
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

               });


            articleProvider = new ArticleProvider(articleRepository.Object, cacheProvider.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArticleProvider_GetFullArticleById_PassNull_Exception()
        {
            articleProvider.GetFullArticleById(null);
        }
        [TestMethod]
        public void ArticleProvider_GetFullArticleById_PassNonExixtingId_NullIsReturned()
        {
            articleRepository
                .Setup(sp => sp.GetFullArticleById(777))
                .Returns(() => { return null; });
            var result=articleProvider.GetFullArticleById(777);
            Assert.IsNull(result);
        }
        [TestMethod]
        [ExpectedException(typeof(DataBaseConnectionException))]
        public void ArticleProvider_GetFullArticleById_DataBaseConnectionEcxeption_Exception()
        {
            throw new DataBaseConnectionException("Error");
        }
        [TestMethod]
        public void ArticleProvider_GetFullArticleById_PassExistingId_NotNullIsReturned()
        {

            var result = articleProvider.GetFullArticleById(1);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ArticleProvider_GetFullArticleById_PassId_GetTargetId()
        {
            int id = 1;
            var result = articleProvider.GetFullArticleById(id);
            Assert.IsTrue(result.Id == id);
        }
        [TestMethod]
        public void ArticleProvider_GetFullArticleById_PassId_InsertDataInCache()
        {
            int id = 1;
            cacheProvider
                .Setup(cp => cp.Insert(It.IsAny<string>(), It.IsAny<ArticleDTO>()));
            articleProvider.GetFullArticleById(1);
            cacheProvider.Verify(cp => cp.Get(cacheKeyGetFullArticleById + id.ToString()), Times.AtLeast(1));
        }

        [TestMethod]
        public void ArticleProvider_GetFullArticleById_PassId_HeadingsIsNotNull()
        {
            var result = articleProvider.GetFullArticleById(1);
            Assert.IsNotNull(result.Headings);
        }



    }
}
