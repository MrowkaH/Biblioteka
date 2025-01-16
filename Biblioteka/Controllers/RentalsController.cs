using Biblioteka.Data;
using Biblioteka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
public class RentalsController : Controller
{
    private readonly ApplicationDbContext _context;

    public RentalsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create(int bookId)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
        if (book == null) return NotFound();

        var rental = new Rental
        {
            BookId = book.Id,
            Book = book,
            BorrowDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14)
        };

        return View(rental);
    }

    [HttpPost]
    public IActionResult Create(Rental rental)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
        if (user == null) return Unauthorized();

        var book = _context.Books.FirstOrDefault(b => b.Id == rental.BookId);
        if (book == null) return NotFound();

        if (book.DostepneKopie <= 0)
        {
            ModelState.AddModelError("", "Brak dostępnych kopii tej książki.");
            return View(rental);
        }

        var newRental = new Rental
        {
            BookId = book.Id,
            UserId = user.Id,
            BorrowDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14),
            ReturnDate = null
        };

        book.DostepneKopie--;

        _context.Rentals.Add(newRental);
        _context.SaveChanges();

        return RedirectToAction("MyRentals", "Rentals");
    }

    [HttpGet]
    public IActionResult MyRentals()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
        if (user == null) return Unauthorized();

        var rentals = _context.Rentals
            .Include(r => r.Book)
            .Where(r => r.UserId == user.Id && r.ReturnDate == null)
            .ToList();

        return View(rentals);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Manage()
    {
        var rentals = _context.Rentals
            .Include(r => r.Book)
            .Include(r => r.User)
            .Where(r => r.ReturnDate == null)
            .ToList();

        return View(rentals);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Cancel(int id)
    {
        var rental = _context.Rentals.FirstOrDefault(r => r.Id == id);
        if (rental == null) return NotFound();

        rental.ReturnDate = DateTime.Now;

        var book = _context.Books.FirstOrDefault(b => b.Id == rental.BookId);
        if (book != null)
        {
            book.DostepneKopie++;
        }

        _context.SaveChanges();
        return RedirectToAction("Manage");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Extend(int id)
    {
        var rental = _context.Rentals.FirstOrDefault(r => r.Id == id);
        if (rental == null) return NotFound();

        rental.DueDate = rental.DueDate.AddDays(7);

        _context.SaveChanges();
        return RedirectToAction("Manage");
    }
}
