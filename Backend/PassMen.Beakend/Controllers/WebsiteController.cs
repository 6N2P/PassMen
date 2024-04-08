using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassMen.Beakend.Data;
using PassMen.Beakend.Model;

namespace PassMen.Beakend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private readonly PassMenDbContext _context;
        public WebsiteController(PassMenDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Website>> Get() => 
         await   _context.Websites.ToListAsync();

        [HttpGet("id/{id}")]
        [ProducesResponseType(typeof(Website), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var userpass = await _context.Websites.FindAsync(id);
            return userpass == null ? NotFound() : Ok(userpass);
        }

        [HttpGet("iduser/{iduser}")]
        [ProducesResponseType(typeof(Website), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByName(int iduser)
        {
            var userpass = await _context.Websites.Where(x => x.IdUser == iduser).FirstOrDefaultAsync();
            return userpass == null ? NotFound() : Ok(userpass);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Website website)
        {
            await _context.Websites.AddAsync(website);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = website.Id }, website);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Website website)
        {
            if (id != website.Id) return BadRequest();
            _context.Entry(website).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
