using BusinessObjects;
using DataObjects;
using DataObjects.EntityFramework;
using Microsoft.AspNet.Identity;
using Mvc.JQuery.Datatables;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebUI.Models;
using WebUI.Repositories;

namespace WebUI.Controllers
{
    public class ReceiptsController : Controller
    {

        private readonly IReceiptRepository _receiptRepository;

        //todo need DI later
        public ReceiptsController()
        {
            _receiptRepository = new ReceiptRepository();
        }

        public ReceiptsController(IReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AutoCompleteSearch(string id, string searchString)
        {
            List<string> list = new List<string>() { "aaa", "bbb", "abc" };
            return Json(new { list }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //todo check json attributes eg hannel json exception
        public async Task<JsonResult> DataTableAjaxHandler(DataTablesParam param) //todo copy DataTablesParam in and remove mvc.jquery.datatables lib
        {
            var userID = User.Identity.GetUserId();
            var recieptBriefList = await _receiptRepository.GetReceiptBriefsByUserAsync(userID);
            var json = GenerateJsonContent(recieptBriefList);
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
            var list = new List<List<string>>();
            foreach (var receiptBrief in data)
            {
                var inner = new List<string>()
                {
                    receiptBrief.Id.ToString(CultureInfo.InvariantCulture),
                    receiptBrief.Description,
                    receiptBrief.PurchaseDate.ToString(),
                    receiptBrief.Price.ToString(),
                    receiptBrief.Vendor,
                    receiptBrief.AccountType,
                    receiptBrief.IsBulk.ToString(),
                    receiptBrief.HasImage,
                    string.Empty
                };
                list.Add(inner);
            }

            return list;
        }

        public async Task<ViewResult> Edit(int id)
        {
            string userID = User.Identity.GetUserId();
            var viewModel = await _receiptRepository.GetReceiptEditAsync(id, userID);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Receipt receipt, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    ReceiptImage ri = new ReceiptImage();
                    ri.UserId = User.Identity.GetUserId();
                    ri.IsActive = true;
                    ri.Description = "dd";
                    ri.ImageMimeType = image.ContentType;
                    ri.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(ri.ImageData, 0, image.ContentLength); //todo image
                    await DataAccess.ReceiptImageDao.SaveAsync(ri);
                }

                if (receipt.Id == 0)
                {
                    receipt.UserId = User.Identity.GetUserId();
                }
                await _receiptRepository.SaveReceiptAsync(receipt);

                //This is a key/value dictionary similar to the session data and view bag features we have used previously.
                //The key difference from session data is that temp data is deleted at the end of the HTTP request.
                TempData["message"] = string.Format("{0} has been saved", receipt.Description);
                return RedirectToAction("Index");
            }

            var receiptEditViewModel = await _receiptRepository.GetReceiptEditPostAsync(User.Identity.GetUserId(), receipt);
            return View(receiptEditViewModel);
        }

        public async Task<ViewResult> Create()
        {
            string userID = User.Identity.GetUserId();
            var viewModel = await _receiptRepository.GetReceiptEditAsync(0, userID);
            return View("Edit", viewModel);
        }

        //[HttpPost]
        public async Task<RedirectToRouteResult> Delete(ReceiptBrief receiptBrief)
        {
            await _receiptRepository.DeleteReceiptAsync(receiptBrief);
            TempData["message"] = string.Format("{0} was deleted", receiptBrief.Description);
            return RedirectToAction("Index");
        }

        public async Task<FileContentResult> GetImage(int id)
        {
            //var userId = User.Identity.GetUserId();
            //var receipt = ReceiptDao.GetReceiptsByUser(userId).FirstOrDefault(r => r.Id == id);

            var receipt = await _receiptRepository.GetReceiptAsync(id);
            if (receipt != null)
            {
                //return File(receipt.ImageData, receipt.ImageMimeType); //todo image
            }

            return null;
        }


    }
}
