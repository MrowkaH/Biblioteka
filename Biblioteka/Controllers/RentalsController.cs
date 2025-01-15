using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    public class RentalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult MyRentals()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManageRentals()
        {
            return View();
        }
    }
}
