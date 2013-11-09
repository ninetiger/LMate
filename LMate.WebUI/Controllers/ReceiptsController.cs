﻿using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;
using LMate.DataObjects.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMate.Controllers
{
    public class ReceiptsController : Controller
    {
         private readonly IReceiptRepository _repository;

         public ReceiptsController()//IReceiptRepository receiptRepository)
        {
            //_repository = receiptRepository;
            _repository = new EFReceiptRepository();
        }

        public ActionResult Index()
        {
            return View(_repository.Receipts);
        }

        public ViewResult Edit(int id)
        {
            Receipt receipt = _repository.Receipts
                                .FirstOrDefault(r => r.Id == id);
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

                _repository.SaveReceipt(receipt);

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
        public ActionResult Delete(int id)
        {
            Receipt deletedReceipt = _repository.DeleteReceipt(id);

            if (deletedReceipt != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedReceipt.Description);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int id)
        {
            var receipt = _repository.Receipts.FirstOrDefault(r => r.Id == id);
            if (receipt != null)
            {
                return File(receipt.ImageData, receipt.ImageMimeType);
            }

            return null;
        }
    }
}
