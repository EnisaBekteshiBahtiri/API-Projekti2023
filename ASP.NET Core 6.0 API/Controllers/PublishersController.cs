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
    public class PublishersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PublishersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult> GetPublishers()
        {
            try
            {
                if (_context.Publishers == null)
                {
                    return NotFound();
                }
                return Ok(await _context.Publishers.ToListAsync());
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
                throw;
            }
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPublisher(int id)
        {
            try
            {
                if (_context.Publishers == null)
                {
                    return NotFound();
                }
                var publisher = await _context.Publishers.FindAsync(id);

                if (publisher == null)
                {
                    return NotFound();
                }

                return Ok(publisher);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
                throw;
            }
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPublisher(CreatePublisherDTO publisher)
        {
            try
            {
                if (_context.Publishers == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Publishers'  is null.");
                }
                var entity = new Publisher()
                {
                    FirstName = publisher.FirstName,
                    LastName = publisher.LastName,
                    Birthday = publisher.Birthday,
                };
                _context.Publishers.Add(entity);
                await _context.SaveChangesAsync();

                return Ok("Addedd successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
                throw;
            }
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            try
            {
                if (_context.Publishers == null)
                {
                    return NotFound();
                }
                var publisher = await _context.Publishers.FindAsync(id);
                if (publisher == null)
                {
                    return NotFound();
                }

                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();

                return Ok("Deleted successfully!");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool PublisherExists(int id)
        {
            return (_context.Publishers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
