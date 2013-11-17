using BusinessObjects;
using DataObjects;
using Microsoft.AspNet.Identity;
using Mvc.JQuery.Datatables;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ReceiptsController : Controller
    {
        private static readonly IReceiptDao ReceiptDao = DataAccess.ReceiptDao;

        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            var recieptList = ReceiptDao.GetReceiptsByUser(userID);
            return View(recieptList);
        }

        public JsonResult AutoCompleteSearch(string id, string searchString)
        {
            List<string> list = new List<string>() {"aaa", "bbb", "abc"};
            return Json(new {list}, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //todo check json attributes eg hannel json exception
        public async Task<JsonResult> DataTableAjaxHandler(DataTablesParam param) //todo copy DataTablesParam in and remove mvc.jquery.datatables lib
        {
            string userID = User.Identity.GetUserId();
            var recieptBriefList = await ReceiptDao.GetReceiptBriefsByUserAsync(userID);
            List<List<string>> json = GenerateJsonContent(recieptBriefList);
             var jsonString = Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = recieptBriefList.Count(),
                iTotalDisplayRecords = 3,
                aaData = json
            },
            JsonRequestBehavior.AllowGet);

            return jsonString;
        }

        private static List<List<string>> GenerateJsonContent(IEnumerable<ReceiptBrief> data)
        {
            List<List<string>> list = new List<List<string>>();
            foreach (var receiptBrief in data)
            {
                List<string> inner = new List<string>();
                inner = new List<string>()
                {
                    receiptBrief.Id.ToString(CultureInfo.InvariantCulture),
                    receiptBrief.Description,
                    receiptBrief.PurchaseDate.ToString(),
                    receiptBrief.Price.ToString(),
                    receiptBrief.Vendor,
                    receiptBrief.ReceiptType,
                    receiptBrief.IsBulk.ToString(),
                    receiptBrief.HasImage,
                    string.Empty
                };
                list.Add(inner);
            }

            return list;
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

        //[HttpPost]
        public async Task<RedirectToRouteResult> Delete(ReceiptBrief receiptBrief)
        {
            await ReceiptDao.DeleteReceiptAsync(receiptBrief);
            TempData["message"] = string.Format("{0} was deleted", receiptBrief.Description);
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
