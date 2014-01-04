using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using WebUI.Common;
using WebUI.Controllers;
using WebUI.Models;
using WebUI.Repositories;
using WebUI.Tests.Data;

namespace WebUI.Tests.Controllers
{
    [TestClass]
    public class ReceiptsControllerTest
    {
        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the 
        /////current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        ///// <summary>
        /////Initialize() is called once during test execution before
        /////test methods in this test class are executed.
        /////</summary>
        //[TestInitialize()]
        //public void Initialize() { }

        ///// <summary>
        /////Cleanup() is called once during test execution after
        /////test methods in this class have executed unless
        /////this test class' Initialize() method throws an exception.
        /////</summary>
        //[TestCleanup()]
        //public void Cleanup() { }

        [TestMethod]
        public void Index_CanCall()
        {
            // Mocking
            var mockReceipt = new Mock<IReceiptRepository>();
            var mockUserPermission = new Mock<IUserPermissionRepository>();

            // Arrange
            var controller = new ReceiptsController(mockReceipt.Object, mockUserPermission.Object);

            // Act
            var viewResult = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void DataTableAjaxHandler()
        {
            // Mocking
            var list = ReceiptBriefViewModelData.GetList();
            var dataTablesParam = new DataTablesParam() { sEcho = 5 };
            var userViewModel = new UserViewModel() { UserId = "uId" };

            var mockRepo = new Mock<IReceiptRepository>();
            var mockUserPermission = new Mock<IUserPermissionRepository>();
            mockRepo.Setup(m => m.GetReceiptBriefsByUserIdAsync("uId")).Returns(Task.FromResult(list));

            // Arrange
            var controller = new ReceiptsController(mockRepo.Object, mockUserPermission.Object);

            // Act
            Task<JsonResult> taskResult = controller.DataTableAjaxHandler(dataTablesParam, userViewModel);
            JsonResult result = taskResult.Result;

            // Assert
            const string expected = "{\"sEcho\":5,\"iTotalRecords\":2,\"iTotalDisplayRecords\":3,\"aaData\":[[\"0\",\"d0\",\"v0\",\"1 Jan 2011\",\"10.10\",\"Yy\",\"1 Feb 2011\",\"Approved\",\"\"],[\"1\",\"d1\",\"v1\",\"\",\"\",null,\"15 Mar 2011\",\"Draft\",\"\"]]}";
            string returnResult = JsonConvert.SerializeObject(result.Data);
            Assert.AreEqual(expected, returnResult);
        }
    }
}
