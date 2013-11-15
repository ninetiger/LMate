using System;
using BusinessObjects;
using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;
using LMate.DataObjects.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataObjects.Tests
{
    [TestClass]
    public class ReceiptRepoTests
    {
        [TestMethod]
        public void Test()
        {
            var receipt = new Receipt
            {
                Id = 0,
                Description = "desc",
                PurchaseDate = DateTime.Now,
                Price = 999
            };

            IReceiptRepository receiptRepository = new EFReceiptRepository();
            receiptRepository.SaveReceipt(receipt);

        }
    }
}
