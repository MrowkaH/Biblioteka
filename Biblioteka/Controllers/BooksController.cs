﻿using Biblioteka.Data;
using Biblioteka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Biblioteka.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
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
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu: {ex.Message}");
                return View(book);
            }

            return RedirectToAction("ManageBooks", "Admin");
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
            var book = _context.Books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (book == null)
                return NotFound();

            book.Tytul = updatedBook.Tytul;
            book.Autor = updatedBook.Autor;
            book.ISBN = updatedBook.ISBN;
            book.DostepneKopie = updatedBook.DostepneKopie;

            _context.SaveChanges();

            return RedirectToAction("ManageBooks", "Admin");
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

            return RedirectToAction("ManageBooks", "Admin");
        }
    }
}
