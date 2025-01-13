using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    public class RentalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
