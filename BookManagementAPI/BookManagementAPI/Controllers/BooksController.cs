using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> Books = new List<Book>();
        private static int IdCounter = 1;

        // GET: api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(Books);
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found." });
            }
            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public ActionResult AddBook([FromBody] Book book)
        {
            book.Id = IdCounter++;
            Books.Add(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found." });
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.ISBN = updatedBook.ISBN;
            book.PublicationDate = updatedBook.PublicationDate;
            return NoContent();
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found." });
            }

            Books.Remove(book);
            return NoContent();
        }
    }
}
