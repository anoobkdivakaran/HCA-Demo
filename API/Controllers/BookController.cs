using HCA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext _dbContext;
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger, BookDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            _logger.LogInformation("Getting all book details.");
            return await _dbContext.Books.ToListAsync();
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
            _logger.LogInformation("Getting book with ID {Id}.", id);
            var book = await _dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // POST: api/<BookController>
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            _logger.LogInformation("Creating book with value {Value}.", book);
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }

        // PUT: api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, Book product)
        {
            _logger.LogInformation("Updating book with ID {Id} and value {Value}.", id, product);
            if (id != product.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(product).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
             _logger.LogInformation("Deleting Book with ID {Id}.", id);
            var product = await _dbContext.Books.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Books.Remove(product);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CheckBookExists(Guid id)
        {
            return _dbContext.Books.Any(e => e.Id == id);
        }

    }
}
