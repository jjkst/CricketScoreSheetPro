﻿using Couchbase.Lite;
using CricketScoreSheetPro.Core;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Test.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CricketScoreSheetPro.Test.UnitTest
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Database client is not set.")]
        [TestCategory("UnitTest")]
        public void RepositoryTest_Null()
        {
            //Act
            var baseRepo = new DataSeedService<object>(null);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Database is not set.")]
        [TestCategory("UnitTest")]
        public void RepositoryTest_DatabaseNull()
        {
            //Arrange
            var mockClient = new Mock<IClient>();
            Database db = null;
            mockClient.Setup(c => c.GetDatabase()).Returns(db);

            //Act
            var baseRepo = new DataSeedService<object>(mockClient.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "UUID is not set.")]
        [TestCategory("UnitTest")]
        public void RepositoryTest_UUIDEmpty()
        {
            //Arrange
            var mockClient = new Mock<IClient>();
            mockClient.Setup(c => c.GetDatabase()).Returns(new Database("rock"));
            mockClient.Setup(c => c.GetUUID()).Returns(string.Empty);

            //Act
            var baseRepo = new DataSeedService<object>(mockClient.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Database is not set.")]
        [TestCategory("UnitTest")]
        public void CreateTest_DatabaseNull()
        {
            //Arrange
            var mockClient = new Mock<IClient>();
            var baseRepo = new DataSeedService<object>(mockClient.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "New property to be added is empty.")]
        [TestCategory("UnitTest")]
        public void CreateTest_Null()
        {
            //Arrange
            var baseRepo = new DataSeedService<object>(new TestClient());

            //Act
            var val = baseRepo.Create(null);
        }
    }
}
