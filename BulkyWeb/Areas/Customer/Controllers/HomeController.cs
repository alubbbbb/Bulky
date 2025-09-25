using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        // Logger zum Protokollieren von Informationen und Fehlern
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        

        // Konstruktor, Dependency Injection für den Logger
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;   
        }

        // Action für die Startseite (Index)
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                ProductId = productId,
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category")
            };
            return View(cartObj);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart cardObj)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            cardObj.ApplicationUserId = userId;

            ShoppingCart cartFromdDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == cardObj.ProductId);
       
            if(cartFromdDb == null)
            {
                _unitOfWork.ShoppingCart.Add(cardObj);
               
            }
            else
            {
                cartFromdDb.Count += cardObj.Count;
                _unitOfWork.ShoppingCart.Update(cartFromdDb);
            }
            TempData["success"] = "Cart updated successfully";
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // Action für die Datenschutzerklärung
        public IActionResult Privacy()
        {
            return View();
        }

        // Action für die Fehlerseite, ResponseCache verhindert das Caching der Fehlerseite
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Erzeugt ein ErrorViewModel-Objekt mit der aktuellen RequestId für die Fehleranzeige
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}