using System.Threading.Tasks;
using System.Web;
using BusinessObjects;
using DataObjects;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ReceiptsController : Controller
    {
        private static readonly IReceiptDao ReceiptDao = DataAccess.ReceiptDao;

        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();

            var model = ReceiptDao.GetReceiptsByUser(userID);
            return View(model);
        }

        public ViewResult Edit(int id)
        {
            var receipt = ReceiptDao.GetReceipt(id);
            return View(receipt);
        }

        [HttpPost]
        public ActionResult Edit(Receipt receipt, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    receipt.ImageMimeType = image.ContentType;
                    receipt.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(receipt.ImageData, 0, image.ContentLength);
                }

                if (receipt.Id == 0)
                {
                    receipt.UserId = User.Identity.GetUserId();
                }
                ReceiptDao.SaveReceipt(receipt);

                //This is a key/value dictionary similar to the session data and view bag features we have used previously.
                //The key difference from session data is that temp data is deleted at the end of the HTTP request.
                TempData["message"] = string.Format("{0} has been saved", receipt.Description);
                return RedirectToAction("Index");
            }

            // there is something wrong with the data values
            return View(receipt);
        }

        public ViewResult Create()
        {
            return View("Edit", new Receipt());
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> Delete(Receipt receipt)
        {
            await ReceiptDao.DeleteReceiptAsync(receipt);
            TempData["message"] = string.Format("{0} was deleted", receipt.Description);
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int id)
        {
            //var userId = User.Identity.GetUserId();
            //var receipt = ReceiptDao.GetReceiptsByUser(userId).FirstOrDefault(r => r.Id == id);

            var receipt = ReceiptDao.GetReceipt(id);
            if (receipt != null)
            {
                return File(receipt.ImageData, receipt.ImageMimeType);
            }

            return null;
        }
    }
}
