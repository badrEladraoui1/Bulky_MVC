using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Company> Companys = _unitOfWork.Company.GetAll().ToList();

            return View(Companys);
        }
        public IActionResult UpSert(int? id) //updateAndInsert
        {
            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company CompanyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(CompanyObj);
            }
            //ViewBag.categorylist = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
        }
        [HttpPost]
        public IActionResult UpSert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {
                if (CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);

                }
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyObj);
            }
        }

        //needs to be deleted

        //public IActionResult Delete(int? Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        NotFound();
        //    }
        //    Company Company = _unitOfWork.Company.Get(x => x.Id == Id);
        //    if (Company == null)
        //    {
        //        NotFound();
        //    }
        //    return View(Company);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? Id)
        //{
        //    Company Company = _unitOfWork.Company.Get(z => z.Id == Id);
        //    if (Company == null)
        //    {
        //        NotFound();
        //    }
        //    _unitOfWork.Company.Remove(Company);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Company Deleted successfully";
        //    return RedirectToAction("Index");
        //}


        //Training

        //public IActionResult Search(string searchQuery)
        //{
        //    if (string.IsNullOrEmpty(searchQuery))
        //    {
        //        // Handle empty search query, you might want to redirect or show a message
        //        return RedirectToAction("Index");
        //    }

        //    var searchResults = _unitOfWork.Company.GetAll()
        //                          .Where(u => u.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
        //                          .ToList();

        //    // Check if there are any search results
        //    if (searchResults.Count == 0)
        //    {
        //        // Redirect to Index or handle no results scenario
        //        return RedirectToAction("Index");
        //    }

        //    return View("Index", searchResults);
        //}


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { succes = false, mesage = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { succes = true, message = "Deleted Successfully" });

        }

        #endregion


    }
}
