using ASP.NET_Core_6._0_API.DTO;
using ASP.NET_Core_6._0_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_6._0_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult> GetBooks([FromQuery] FilterBookDTO? model = null)
        {
            try
            {
                if (_context.Books == null)
                {
                    return NotFound();
                }
                if (model == null)
                {
                    return Ok(await _context.Books.ToListAsync());
                }
                else
                {
                    var list = await _context.Books.Include(x => x.Language).Where(x =>
                    (!string.IsNullOrEmpty(model.Name) ? x.Name.Contains(model.Name) : true) &&
                    (model.PublisherId.HasValue ? x.PublisherId == model.PublisherId : true) &&
                    (model.PublishDate.HasValue ? x.PublishDate == model.PublishDate : true) &&
                    (!string.IsNullOrEmpty(model.Isbn) ? x.Isbn.Contains(model.Isbn) : true) &&
                    (model.LanguageId.HasValue ? x.LanguageId == model.LanguageId : true) &&
                    (model.NumberOfPages.HasValue ? x.NumberOfPages == model.NumberOfPages : true) &&
                    (!string.IsNullOrEmpty(model.Description) ? (x.Description ?? "").Contains(model.Description) : true) &&
                    (!string.IsNullOrEmpty(model.LanguageName) ? (x.Language.Name ?? "").Contains(model.LanguageName) : true)
                    ).ToListAsync();

                    return Ok(list);
                }
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
                throw;
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBook(int id)
        {
            try
            {
                if (_context.Books == null)
                {
                    return NotFound();
                }
                var book = await _context.Books.FindAsync(id);

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
                throw;
            }
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostBook(CreateBookDTO book)
        {
            try
            {
                if (_context.Books == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Books'  is null.");
                }
                var entity = new Book()
                {
                    PublisherId = book.PublisherId,
                    Name = book.Name,
                    Description = book.Description,
                    Isbn = book.Isbn,
                    LanguageId = book.LanguageId,
                    NumberOfPages = book.NumberOfPages,
                    PublishDate = book.PublishDate
                };

                _context.Books.Add(entity);
                await _context.SaveChangesAsync();

                return Ok("Addedd successfully!");
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                if (_context.Books == null)
                {
                    return NotFound();
                }
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return Ok("Deleted successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
                throw;
            }
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
