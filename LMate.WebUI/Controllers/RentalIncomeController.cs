using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;
using LMate.DataObjects.Concrete;

namespace LMate.WebUI.Controllers
{
    public class RentalIncomeController : Controller
    {
        private readonly IRentalIncomeRepository _repository;

        public RentalIncomeController()//IRentalIncomeRepository rentalIncomeRepository)
        {
            //_repository = rentalIncomeRepository;
            _repository = new EFRentalIncomeRepository();
        }

        public ActionResult Index()
        {
            return View(_repository.RentalIncomes);
        }

        public ViewResult Edit(int id)
        {
            RentalIncome rentalIncome = _repository.RentalIncomes
                                .FirstOrDefault(r => r.ID == id);
            return View(rentalIncome);
        }

        [HttpPost]
        public ActionResult Edit(RentalIncome rentalIncome)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveRentalIncome(rentalIncome);

                TempData["message"] = string.Format("Rental income, year ended {0}, has been saved", rentalIncome.YearEnded);
                return RedirectToAction("Index");
            }
            
            return View(rentalIncome);
        }

        public ViewResult Create()
        {
            return View("Edit", new RentalIncome());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            RentalIncome deletedRentalIncome = _repository.DeleteRentalIncome(id);

            if (deletedRentalIncome != null)
            {
                TempData["message"] = string.Format("Rental income, year ended {0}, was deleted", deletedRentalIncome.YearEnded);
            }
            return RedirectToAction("Index");
        }
    }
}