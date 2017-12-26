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
    public class HeadingProviderGetHeadingsTest
    {
        private IHeadingProvider headingProvider;
        private Mock<IHeadingRepository> headingRepository;
        private Mock<ICacheProvider> cacheProvider;
        
        [TestInitialize]
        public void TestInitialize()
        {
            headingRepository = new Mock<IHeadingRepository>();
            cacheProvider = new Mock<ICacheProvider>();
            headingRepository
                .Setup(sp => sp.GetHeadings())
                .Returns(new List<Heading>()
                {
                    new Heading()
                    {
                        Id = 1,
                        Name = "Travelling",
                        Description="All about travelling"
                    },
                    new Heading()
                    {
                        Id = 2,
                        Name = "History",
                        Description="All about history"
                    }
                }.ToArray());
            headingProvider = new HeadingProvider(headingRepository.Object, cacheProvider.Object);
        }

        [TestMethod]
        public void HeadingProvider_GetHeadings_GetNullFromServiceProvider_NullIsReturned()
        {
            headingRepository.Setup(sp => sp.GetHeadings()).Returns(() => { return null; });
            var result = headingProvider.GetHeadings();
            Assert.IsNull(result);
        }
        [TestMethod]
        public void HeadingProvider_GetHeadings_GetNotNullFromServiceProvider_NotNullIsReturned()
        {
            var result = headingProvider.GetHeadings();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        [ExpectedException(typeof(DataBaseConnectionException))]
        public void HeadingProvider_GetHeadings_DataBaseConnectionException_Exception()
        {
            throw new DataBaseConnectionException("Error");            
        }
        [TestMethod]
        public void HeadingProvider_GetHeadings_HeadingNameIsNotEmpty()
        {
            var result = headingProvider.GetHeadings();
            foreach(var item in result)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(item.Name));
            }
        }
    }
}
