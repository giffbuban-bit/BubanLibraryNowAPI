using BubanLibraryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BubanLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
             new Book
            {
                Id = 1,
                       Title = "Ibong adarna",
                       Author = " José de la Cruz ",
                          Genre = "Corrid ",
                            Available = true,
                                PublisherYear = 1980
            },
            new Book
            {
            Id = 2,
                       Title = "The Man and the Hero s journey ",
                       Author = " Jose Rizal",
                          Genre = " Biography",
                            Available = true,
                                PublisherYear = 2006

        }
        };
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            { status = "success",
                data = books,
                   message = "Books retrieved."

            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });

            }
            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book retrieved."
            });
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.Id },
                new { status = "success",
                    data = newBook,
                    message = "Book created."
                });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,
            [FromBody] Book updatedBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            }
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.Available = updatedBook.Available;
            book.PublisherYear = updatedBook.PublisherYear;
            
            return Ok(new
            {
                status = "success",
                data = book,
                    message = "Book updated."
                });

            
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            }
            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Book deleted."
            });
        }

    }
}
