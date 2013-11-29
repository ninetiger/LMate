using System;
using System.Collections.ObjectModel;
using System.Drawing;
using BusinessObjects;
using DataObjects;
using DataObjects.EntityFramework;
using DataObjects.EntityFramework.ModelMapper;
using DataObjects.Shared;
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
using Receipt = DataObjects.EntityFramework.Receipt;

namespace WebUI.Controllers
{
    public class ReceiptsController : Controller
    {
        private ReceiptRepository _efReceiptRepository;

        //todo need DI later
        public ReceiptsController()
        {
            _efReceiptRepository = new ReceiptRepository();
        }
        //todo need a destructor???

        public ReceiptsController(ReceiptRepository efReceiptRepository)
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
        public async Task<JsonResult> DataTableAjaxHandler(DataTablesParam param) //todo copy DataTablesParam in and remove mvc.jquery.datatables lib
        {
            var userId = User.Identity.GetUserId();
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
                receiptBrief.Id.ToString(), 
                receiptBrief.Description, 
                receiptBrief.PurchaseDate.ToString(),
                receiptBrief.Price.ToString(), 
                receiptBrief.Vendor, 
                receiptBrief.AccountType, 
                receiptBrief.IsBulk.ToString(), 
                receiptBrief.HasImage, 
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
        public async Task<ActionResult> Edit(ReceiptViewModel receiptViewModel, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                receiptViewModel.UserId = User.Identity.GetUserId();

                if (image != null)
                {
                    var receiptImage = new ReceiptImage
                    {
                        ImageData = new byte[image.ContentLength],
                        ImageMimeType = image.ContentType,
                        Description = "dd",
                        Date = DateTime.Now,
                        IsActive = true,
                        User_Id = User.Identity.GetUserId()
                    };
                    image.InputStream.Read(receiptImage.ImageData, 0, image.ContentLength); //todo image
                    await _efReceiptRepository.InsertImage(receiptImage, receiptViewModel.Id);
                }

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
            if (!receiptBrief.Id.HasValue) return RedirectToAction("Index");


            await _efReceiptRepository.DeleteAsync(receiptBrief.Id.Value);
            await _efReceiptRepository.SaveChangesAsync();

            TempData["message"] = string.Format("{0} was deleted", receiptBrief.Description);
            return RedirectToAction("Index");
        }

        public async Task<FileContentResult> GetImage(int imageId)
        {
            var image = await _efReceiptRepository.GetImage(imageId);
            if (image != null)
            {
                return File(image.ImageData, image.ImageMimeType);
            }

            return null;
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
