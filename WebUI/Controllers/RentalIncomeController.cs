using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;
using LMate.DataObjects.Concrete;

namespace WebUI.Controllers
{
    [Authorize]
    public class RentalIncomeController : Controller
    {
        private readonly IRentalIncomeDetailsRepository _repository;

        public RentalIncomeController()//IRentalIncomeRepository rentalIncomeRepository)
        {
            //_repository = rentalIncomeRepository;
            _repository = new EFRentalIncomeDetailRepository();
        }

        public ActionResult Index()
        {
            return View(_repository.RentalIncomes);
        }

        public ViewResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var rentalIncomeDetail = _repository.RentalIncomeDetails
                    .FirstOrDefault(r => r.Id == id);
                return View(rentalIncomeDetail);
            }
            else
            {
                var prevYearDetail = _repository.GetNewRentalIncomeDetailBasedOnPrevYear();
                return View(prevYearDetail ?? new RentalIncomeDetail());
            }
        }

        [HttpPost]
        public ActionResult Edit(RentalIncomeDetail rentalIncomeDetail)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveRentalIncomeDetail(rentalIncomeDetail);

                TempData["message"] = string.Format("Rental income, year ended {0}, has been saved", rentalIncomeDetail.YearEnded.ToString("yyyy"));
                return RedirectToAction("Index");
            }
            
            return View(rentalIncomeDetail);
        }

        //public ViewResult Create()
        //{
        //    var prevYearDetail = _repository.GetNewRentalIncomeDetailBasedOnPrevYear();
        //    return View("Edit", prevYearDetail ?? new RentalIncomeDetail());
        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var deletedRentalIncomeDetail = _repository.DeleteRentalIncomeDetail(id);

            if (deletedRentalIncomeDetail != null)
            {
                TempData["message"] = string.Format("Rental income, year ended {0}, was deleted", deletedRentalIncomeDetail.YearEnded.ToString("yyyy"));
            }
            return RedirectToAction("Index");
        }
    }
}