using System;
using BusinessObjects;
using DataObjects.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebUI.Common;
using WebUI.Models;
using WebUI.Repositories;

namespace WebUI.Controllers
{
    public class ReceiptsController : Controller
    {
        private IReceiptRepository _efReceiptRepository;

        public ReceiptsController(IReceiptRepository efReceiptRepository)
        {
            _efReceiptRepository = efReceiptRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AutoCompleteSearch(string id, string searchString)
        {
            var list = new List<string>() { "aaa", "bbb", "abc" };
            return Json(new { list }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //todo check json attributes eg hannel json exception
        public async Task<JsonResult> DataTableAjaxHandler(DataTablesParam param, UserViewModel user)
        {
            var userId = user.UserId;
            var recieptBriefList = await _efReceiptRepository.GetReceiptBriefsByUserIdAsync(userId);

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

        private static List<List<string>> GenerateJsonContent(IEnumerable<ReceiptBriefViewModel> data)
        {
            return data.Select(receiptBrief => new List<string>()
            {
                receiptBrief.Id.ToString(CultureInfo.InvariantCulture), 
                receiptBrief.Description, 
                receiptBrief.Vendor, 
                receiptBrief.PurchaseDate.HasValue ? receiptBrief.PurchaseDate.Value.ToString("d MMM yyyy") : string.Empty,
                receiptBrief.Price.HasValue ? receiptBrief.Price.Value.ToString("#0.00") : string.Empty, 
                receiptBrief.HasImage, 
                receiptBrief.DateEntered.ToString("d MMM yyyy"),
                receiptBrief.Status, 
                string.Empty
            }).ToList();
        }

        public async Task<ViewResult> Edit(int id)
        {
            string userID = User.Identity.GetUserId();
            var viewModel = await _efReceiptRepository.GetReceiptForEditAsync(id, userID);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ReceiptViewModel receiptViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                receiptViewModel.UserId = userId;

                if (receiptViewModel.Id == 0)
                {
                    _efReceiptRepository.Insert(receiptViewModel);
                }
                else
                {
                    await _efReceiptRepository.Update(receiptViewModel);
                }

                await _efReceiptRepository.SaveChangesAsync();

                //This is a key/value dictionary similar to the session data and view bag features we have used previously.
                //The key difference from session data is that temp data is deleted at the end of the HTTP request.
                TempData["message"] = string.Format("{0} has been saved", receiptViewModel.Description);
                return RedirectToAction("Index");
            }

            var receiptEditViewModel = await _efReceiptRepository.GetReceiptForEditViewModelAsync(receiptViewModel);
            return View(receiptEditViewModel);
        }

        public async Task<ViewResult> Create()
        {
            string userID = User.Identity.GetUserId();
            var viewModel = await _efReceiptRepository.GetReceiptForEditAsync(0, userID);
            return View("Edit", viewModel);
        }

        //[HttpPost]
        public async Task<RedirectToRouteResult> Delete(ReceiptBriefViewModel receiptBrief)
        {

            if (receiptBrief.Id < 1) return RedirectToAction("Index"); //TODO LOG it

            var receiptViewModel = new ReceiptViewModel()
            {
                Id = receiptBrief.Id,
                UserId = User.Identity.GetUserId()
            };
            await _efReceiptRepository.DeleteAsync(receiptViewModel);
            await _efReceiptRepository.SaveChangesAsync();

            TempData["message"] = string.Format("{0} was deleted", receiptBrief.Description);
            return RedirectToAction("Index");

        }

        public async Task<FileContentResult> GetImage(int imageId)
        {
            var userId = User.Identity.GetUserId();
            var image = await _efReceiptRepository.GetImageSecure(imageId, userId);
            if (image != null)
            {
                return File(image.ImageData, image.ImageMimeType);
            }

            return null;
        }

        public async Task<string> GetImageAddrsByReceiptId(int receiptId)
        {
            string result = string.Empty;
            if (receiptId > 0)
            {
                var userId = User.Identity.GetUserId();
                result = await _efReceiptRepository.GetImageAddrsByReceiptId(receiptId, userId);
            }
            return result;
        }

        [HttpPost]
        public async Task UploadFiles(int? receiptId, string desc, IEnumerable<HttpPostedFileBase> files)
        {
            //todo need to check the receipt belong to this user
            int id = receiptId ?? 0;
            if (id <= 0)
            {
                //todo log
                return;
            }

            HttpPostedFileBase image = files.FirstOrDefault();
            if (image != null && image.ContentLength > 0)
            {
                //string savedFileName = Path.Combine(
                //  AppDomain.CurrentDomain.BaseDirectory + "Files",
                //  Path.GetFileName(image.FileName));
                //image.SaveAs(savedFileName);

                var userId = User.Identity.GetUserId();

                var receiptImage = new ReceiptImage
                {
                    ImageData = new byte[image.ContentLength],
                    ImageMimeType = image.ContentType,
                    Description = desc,
                    Date = DateTime.Now,
                    IsActive = true,
                    User_Id = userId,
                };

                image.InputStream.Read(receiptImage.ImageData, 0, image.ContentLength);
                await _efReceiptRepository.InsertImage(receiptImage, id, userId);
                await _efReceiptRepository.SaveChangesAsync();
            }
        }

        public async Task DetachAnImage(int imageId, int receiptId)
        {
            var userId = User.Identity.GetUserId();
            await _efReceiptRepository.DetachAnImageFromReceipt(imageId, receiptId, userId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _efReceiptRepository != null)
            {
                _efReceiptRepository.Dispose();
                _efReceiptRepository = null;
            }
            base.Dispose(disposing);
        }
    }
}
