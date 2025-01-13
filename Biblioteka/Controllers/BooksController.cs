using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
