using System.Diagnostics;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        // Logger zum Protokollieren von Informationen und Fehlern
        private readonly ILogger<HomeController> _logger;

        // Konstruktor, Dependency Injection für den Logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action für die Startseite (Index)
        public IActionResult Index()
        {
            return View();
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