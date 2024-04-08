using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassMen.Beakend.Data;
using PassMen.Beakend.Model;

namespace PassMen.Beakend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly PassMenDbContext _context;
        public ApplicationController(PassMenDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Application>> Get() =>
         await _context.Applications.ToListAsync();

        [HttpGet("id/{id}")]
        [ProducesResponseType(typeof(Application), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var userpass = await _context.Applications.FindAsync(id);
            return userpass == null ? NotFound() : Ok(userpass);
        }

        [HttpGet("iduser/{iduser}")]
        [ProducesResponseType(typeof(Application), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByName(int iduser)
        {
            var userpass = await _context.Applications.Where(x => x.IdUser == iduser).FirstOrDefaultAsync();
            return userpass == null ? NotFound() : Ok(userpass);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Application userpass)
        {
            await _context.Applications.AddAsync(userpass);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = userpass.Id }, userpass);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Application application)
        {
            if (id != application.Id) return BadRequest();
            _context.Entry(application).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
