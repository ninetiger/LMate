using System;
using System.Linq;
using System.Threading.Tasks;
using DataObjects.EntityFramework;
using DataObjects.EntityFramework.Implementation;
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

            //IReceiptRepository receiptRepository = new EFReceiptRepository();
            //receiptRepository.SaveReceipt(receipt);

            IDao<Receipt> dao = new EntityReceiptDao(new LMateEntities());
            var list = dao.GetAsync(null, null, "ReceiptCategory");

        }
    }
}
