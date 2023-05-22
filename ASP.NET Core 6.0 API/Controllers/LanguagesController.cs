using ASP.NET_Core_6._0_API.DTO;
using ASP.NET_Core_6._0_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_6._0_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LanguagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Languages
        [HttpGet]
        public async Task<ActionResult> GetLanguages()
        {
            try
            {
                if (_context.Languages == null)
                {
                    return NotFound();
                }
                return Ok(await _context.Languages.ToListAsync());
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
                throw;
            }
        }

        // GET: api/Languages/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetLanguage(int id)
        {
            try
            {
                if (_context.Languages == null)
                {
                    return NotFound();
                }
                var language = await _context.Languages.FindAsync(id);

                if (language == null)
                {
                    return NotFound();
                }

                return Ok(language);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
                throw;
            }
        }

        // PUT: api/Languages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguage(int id, Language language)
        {
            if (id != language.Id)
            {
                return BadRequest();
            }

            _context.Entry(language).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageExists(id))
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

        // POST: api/Languages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostLanguage(CreateLanguageDTO language)
        {
            try
            {
                if (_context.Languages == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Languages'  is null.");
                }
                var entity = new Language()
                {
                    Name = language.Name,
                };
                _context.Languages.Add(entity);
                await _context.SaveChangesAsync();

                return Ok("Addedd successfully!");
            }
            catch (Exception)
            {
                return BadRequest("something went wrong!");
                throw;
            }
        }

        // DELETE: api/Languages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            try
            {
                if (_context.Languages == null)
                {
                    return NotFound();
                }
                var language = await _context.Languages.FindAsync(id);
                if (language == null)
                {
                    return NotFound();
                }

                _context.Languages.Remove(language);
                await _context.SaveChangesAsync();

                return Ok("Deleted successfully!");

            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
                throw;
            }
        }
        private bool LanguageExists(int id)
        {
            return (_context.Languages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
