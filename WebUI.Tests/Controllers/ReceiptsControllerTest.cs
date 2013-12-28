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
        [TestMethod]
        public void Index_CanCall()
        {
            // Mocking
            var mock = new Mock<IReceiptRepository>();

            // Arrange
            var controller = new ReceiptsController(mock.Object);

            // Act
            var viewResult = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(viewResult);
        }

        //[TestMethod]
        //public void AutoCompleteSearch()
        //{
        //    // Mocking
        //    var mock = new Mock<IReceiptRepository>();

        //    // Arrange
        //    var controller = new ReceiptsController(mock.Object);

        //    // Act
        //    var viewResult = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(viewResult);
        //}

        [TestMethod]
        public void DataTableAjaxHandler()
        {
            // Mocking
            var list = ReceiptBriefViewModelData.GetList();
            var dataTablesParam = new DataTablesParam() { sEcho = 5 };
            var userViewModel = new UserViewModel() { UserId = "uId" };

            var mockRepo = new Mock<IReceiptRepository>();
            mockRepo.Setup(m => m.GetReceiptBriefsByUserIdAsync("uId")).Returns(Task.FromResult(list));

            // Arrange
            var controller = new ReceiptsController(mockRepo.Object);

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
