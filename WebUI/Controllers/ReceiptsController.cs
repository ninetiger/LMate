using BusinessObjects;
using DataObjects.EntityFramework;
using System;
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
    [Authorize]
    public class ReceiptsController : Controller
    {
        private IReceiptRepository _efReceiptRepository;
        private readonly IUserPermissionRepository _efUserPermissionRepository;

        public ReceiptsController(IReceiptRepository efReceiptRepository, IUserPermissionRepository efUserPermissionRepository)
        {
            _efReceiptRepository = efReceiptRepository;
            _efUserPermissionRepository = efUserPermissionRepository;
        }

        #region Index page
        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult AutoCompleteReceiptSearch(string id, string searchString, UserViewModel user) //todo need userId secure
        //{
        //    var list = new List<string> { "aaa", "bbb", "abc" };
        //    return Json(new { list }, JsonRequestBehavior.AllowGet);
        //}

        //todo check json attributes eg hannel json exception
        [HttpPost]
        public async Task<JsonResult> DataTableAjaxHandler(DataTablesParam param, UserViewModel user)
        {
            var userId = await ImpersonateCheckAsync(user.UserId);
            var recieptBriefList = await _efReceiptRepository.GetReceiptBriefsByUserIdAsync(userId);

            var json = GenerateJsonContent(recieptBriefList);
            var jsonString = Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = recieptBriefList.Count(),
                iTotalDisplayRecords = 3,
                aaData = json
            });

            return jsonString;
        }

        //todo possible to mave this into the userPermissionRepo
        private async Task<string> ImpersonateCheckAsync(string userId)
        {
            string id = userId;
            var permissionId = Session["PermissionId"];

            if (permissionId == null || string.Empty.Equals(permissionId.ToString()))
            {
                return id;
            }

            var userPermission = await _efUserPermissionRepository.GetUserPermissionSecureAsync(userId, permissionId.ToString());
            if (userPermission != null)
            {
                return userPermission.ActAsUser_Id;
            }

            return id;
        }

        private static List<List<string>> GenerateJsonContent(IEnumerable<ReceiptBriefViewModel> data)
        {
            return data.Select(receiptBrief => new List<string>
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

        public async Task<ViewResult> Create(UserViewModel user)
        {
            var userId = await ImpersonateCheckAsync(user.UserId);
            var viewModel = await _efReceiptRepository.GetReceiptForEditAsync(0, userId);
            return View("Edit", viewModel);
        }

        //[HttpPost]
        public async Task<RedirectToRouteResult> Delete(ReceiptBriefViewModel receiptBrief, UserViewModel user)
        {

            if (receiptBrief.Id < 1) return RedirectToAction("Index"); //TODO LOG it

            var userId = await ImpersonateCheckAsync(user.UserId);

            var receiptViewModel = new ReceiptViewModel
            {
                Id = receiptBrief.Id,
                UserId = userId
            };
            await _efReceiptRepository.DeleteAsync(receiptViewModel);
            await _efReceiptRepository.SaveChangesAsync();

            TempData["message"] = string.Format("{0} was deleted", receiptBrief.Description);
            return RedirectToAction("Index");

        }

        #endregion

        #region Edit page

        public async Task<ViewResult> Edit(int id, UserViewModel user)
        {
            var userId = await ImpersonateCheckAsync(user.UserId);
            var viewModel = await _efReceiptRepository.GetReceiptForEditAsync(id, userId);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ReceiptViewModel receiptViewModel, UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var userId = await ImpersonateCheckAsync(user.UserId);
                receiptViewModel.UserId = userId;

                await UpdateVendorAsync(receiptViewModel, userId);

                //todo receiptstatus set to 1 when add new; why id start from 1000?
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

        private async Task UpdateVendorAsync(ReceiptViewModel receiptViewModel, string userId)
        {
            if (!string.IsNullOrEmpty(receiptViewModel.VendorName))
            {
                var vendor = await _efReceiptRepository.GetVednorSecure(receiptViewModel.VendorName, userId);
                if (vendor == null)
                {
                    var newVednor = new VendorViewModel
                    {
                        VendorName = receiptViewModel.VendorName,
                        UserId = userId
                    };
                    receiptViewModel.VendorId = 0;
                    receiptViewModel.Vendor = newVednor;
                }
                else
                {
                    receiptViewModel.VendorId = vendor.Id;
                }
            }
            else
            {
                receiptViewModel.VendorId = null;
            }
        }

        #endregion

        #region Vendor

        public async Task<JsonResult> AutoCompleteVendor(string searchString, UserViewModel user)
        {
            var userId = await ImpersonateCheckAsync(user.UserId);
            var list = await _efReceiptRepository.SearchVendorNameSecure(searchString, userId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task DeleteVendor(string name, UserViewModel user)
        {
            var userId = await ImpersonateCheckAsync(user.UserId);
            await _efReceiptRepository.DeleteVendorSecure(name, userId);
            await _efReceiptRepository.SaveChangesAsync();
        }

        #endregion

        #region images

        public async Task<FileContentResult> GetImage(int imageId, UserViewModel user)
        {
            var userId = await ImpersonateCheckAsync(user.UserId);

            var image = await _efReceiptRepository.GetImageSecure(imageId, userId);
            if (image != null)
            {
                return File(image.ImageData, image.ImageMimeType);
            }

            return null;
        }

        public async Task<JsonResult> GetImages(int receiptId, UserViewModel user)
        {
            if (receiptId > 0)
            {
                var userId = await ImpersonateCheckAsync(user.UserId);
                var list = await _efReceiptRepository.GetImageAddrsByReceiptId(receiptId, userId);
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            return null; //todo ajax return raise error event, what's the best way to handel?
        }

        [HttpPost]
        public async Task UploadFiles(int? receiptId, string desc, IEnumerable<HttpPostedFileBase> files, UserViewModel user)
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

                var userId = await ImpersonateCheckAsync(user.UserId);

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

        public async Task DetachAnImage(int imageId, int receiptId, UserViewModel user)
        {
            var userId = await ImpersonateCheckAsync(user.UserId);
            await _efReceiptRepository.DetachAnImageFromReceipt(imageId, receiptId, userId);
        }
        #endregion

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
