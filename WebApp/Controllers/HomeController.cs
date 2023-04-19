using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    // HomeController ist ein Controller für die Startseite der Webanwendung
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // Logger zur Protokollierung von Ereignissen

        // Konstruktor für den HomeController, der einen Logger als Abhängigkeit injiziert (DI)
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action-Methode für die Index-Seite der Webanwendung (AM = "Endpunkt" für HTTP-Anfrage)
        public IActionResult Index()
        {
            return View(); // Liefert die Index-View zurück
        }

        // Action-Methode für die Privacy-Seite der Webanwendung (AM = "Endpunkt" für HTTP-Anfrage)
        public IActionResult Privacy()
        {
            return View(); // Liefert die Privacy-View zurück
        }

        // Die Anotation setzt das Caching für diese Methode auf Null
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // Action-Methode für die Error-Seite der Webanwendung (AM = "Endpunkt" für HTTP-Anfrage)
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            // Liefert eine View mit einem ErrorViewModel zurück, das eine Fehlermeldung enthält
        }
    }
}
