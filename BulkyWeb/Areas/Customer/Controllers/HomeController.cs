using System.Diagnostics;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
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
            Product product = _unitOfWork.Product.Get(u=> u.Id == productId, includeProperties: "Category");
            return View(product);
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