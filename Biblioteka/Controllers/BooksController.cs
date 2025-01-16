using Biblioteka.Data;
using Biblioteka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Biblioteka.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BooksController> _logger;

        public BooksController(ApplicationDbContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var availableBooks = _context.Books
                .Where(b => b.DostepneKopie > 0)
                .ToList();
            return View(availableBooks);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Book book)
        {
          /*  if (!ModelState.IsValid)
            {
                return View(book);
            }*/

            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                ModelState.AddModelError(string.Empty, "zle jest debilu");
                return View(book);
            }

            return RedirectToAction(nameof(AdminController.ManageBooks), "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Book updatedBook)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedBook);
            }

            var book = _context.Books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (book == null)
                return NotFound();

            book.Tytul = updatedBook.Tytul;
            book.Autor = updatedBook.Autor;
            book.ISBN = updatedBook.ISBN;
            book.DostepneKopie = updatedBook.DostepneKopie;

            _context.SaveChanges();

            return RedirectToAction(nameof(AdminController.ManageBooks), "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(AdminController.ManageBooks), "Admin");
        }
    }
}