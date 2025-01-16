using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Biblioteka.Data;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var stats = new
            {
                BookCount = _context.Books.Count(),
                UserCount = _context.Users.Count(),
                RentalCount = _context.Rentals.Count(),
                ActiveRentals = _context.Rentals.Count(r => r.ReturnDate == null)
            };

            return View(stats);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ManageBooks()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        public IActionResult ManageUsers()
        {
            var users = _context.Users.Include(u => u.Role).ToList();
            return View(users);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(int id)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(User updatedUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user == null)
                return NotFound();

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.RoleId = updatedUser.RoleId;

            _context.SaveChanges();

            return RedirectToAction("ManageUsers");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUserConfirmed(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("ManageUsers");
        }
        public IActionResult RentalsByUser()
        {
            var query = _context.Rentals
                .Where(r => r.ReturnDate == null) 
                .GroupBy(r => r.UserId)
                .Select(g => new {
                    UserId = g.Key,
                    Count = g.Count()
                }).ToList();

            return View(query);
        }

    }
}
