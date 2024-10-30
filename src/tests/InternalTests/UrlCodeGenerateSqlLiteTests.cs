// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InternalTests
//  Author           : RzR
//  Created On       : 2024-10-23 21:47
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-10-23 21:47
// ***********************************************************************
//  <copyright file="UrlCodeGenerateSqlLiteTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using DomainCommonExtensions.DataTypeExtensions;
using LinkShortener;
using LinkShortener.Abstractions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InternalTests
{
    [TestClass]
    public class UrlCodeGenerateSqlLiteTests
    {
        private ILinkInfoStoreProcessor _linkInfoStoreProcessor;

        [TestInitialize]
        public void Init()
        {
            var dbConnectionString = "Data Source=H:\\DataBases\\SqlLite\\DbStoreUseSqlLiteTest.db";
            var connection = new SqliteConnection(dbConnectionString);

            var sp = new ServiceCollection();
            sp.RegisterLinkShortenerService(connection);

            var serviceProvider = sp.BuildServiceProvider();
            _linkInfoStoreProcessor = serviceProvider.GetRequiredService<ILinkInfoStoreProcessor>();
        }

        [DataRow(10, "cv.iamrzr.dev")]
        [TestMethod]
        public void GenerateUrlKey(int length, string url)
        {
            var urlKey = _linkInfoStoreProcessor.GenerateUrlKey(url, length);
            Assert.IsTrue(urlKey.IsSuccess);
            Assert.IsTrue(urlKey.Response.IsNullOrEmpty().IsFalse());
            Assert.AreEqual(length, urlKey.Response.Length);
        }

        [DataRow(10, "cv.iamrzr.dev")]
        [TestMethod]
        public void GenerateUrlKeyAndDisable(int length, string url)
        {
            var urlKey = _linkInfoStoreProcessor.GenerateUrlKey(url, length);
            Assert.IsTrue(urlKey.IsSuccess);
            Assert.IsTrue(urlKey.Response.IsNullOrEmpty().IsFalse());
            Assert.AreEqual(length, urlKey.Response.Length);

            var disableResult = _linkInfoStoreProcessor.DisableUrl(urlKey.Response);
            Assert.IsTrue(disableResult.IsSuccess);

            var existStoredKey = _linkInfoStoreProcessor.ExistsAny(urlKey.Response, url);
            Assert.IsTrue(existStoredKey.IsSuccess);
            Assert.IsTrue(existStoredKey.Response);
        }

        [DataRow(10, "cv.iamrzr.dev")]
        [TestMethod]
        public void GenerateUrlKeyCheckAndDisable(int length, string url)
        {
            var urlKey = _linkInfoStoreProcessor.GenerateUrlKey(url, length);
            Assert.IsTrue(urlKey.IsSuccess);
            Assert.IsTrue(urlKey.Response.IsNullOrEmpty().IsFalse());
            Assert.AreEqual(length, urlKey.Response.Length);

            var storedUrl = _linkInfoStoreProcessor.GetUrlByCode(urlKey.Response);
            Assert.IsTrue(storedUrl.IsSuccess);
            Assert.IsTrue(storedUrl.Response.IsNullOrEmpty().IsFalse());
            Assert.AreEqual(url, storedUrl.Response);

            var disableResult = _linkInfoStoreProcessor.DisableUrl(urlKey.Response);
            Assert.IsTrue(disableResult.IsSuccess);

            var existStoredKey = _linkInfoStoreProcessor.ExistsAny(urlKey.Response, url);
            Assert.IsTrue(existStoredKey.IsSuccess);
            Assert.IsTrue(existStoredKey.Response);
        }
    }
}