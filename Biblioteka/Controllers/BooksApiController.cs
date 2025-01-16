using Biblioteka.Data;
using Biblioteka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BooksApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<BooksApiController> _logger;

    public BooksApiController(ApplicationDbContext context, ILogger<BooksApiController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        var books = _context.Books.ToList();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetBook(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateBook([FromBody] Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
            return StatusCode(500, "zletworzy");
        }

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound();

        book.Tytul = updatedBook.Tytul;
        book.Autor = updatedBook.Autor;
        book.ISBN = updatedBook.ISBN;
        book.DostepneKopie = updatedBook.DostepneKopie;

        try
        {
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
            return StatusCode(500, "editzly");
        }

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound();

        try
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
            return StatusCode(500, "zle usuwa ksiazki");
        }

        return NoContent();
    }
}

