using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return View(objCompanyList);
        }
        
        public IActionResult Upsert(int? id)
        {
          
            
                if (id == null || id == 0)
                {
                    //create  
                    return View(new Company());
                }
                else
                {
                    //update
                    Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                    return View(companyObj);
                }
        }


        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
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
                TempData["success"] = "Company created succesfully";
                return RedirectToAction("Index");

            } else
                return View(CompanyObj);
        }
           
        
            
           
                
            
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
            {
                var CompanyList = _unitOfWork.Company.GetAll();
                return Json(new { data = CompanyList });
            }
        #endregion

        #region API CALLS
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { sucess = false, message = "Erorr while deleting" });
            }

            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { sucess = true, message = "Delete Successful" });

        }
        #endregion
    }
}