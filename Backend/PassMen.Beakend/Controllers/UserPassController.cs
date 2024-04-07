using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassMen.Beakend.Data;
using PassMen.Beakend.Model;

namespace PassMen.Beakend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPassController : ControllerBase
    {
        private readonly PassMenDbContext _context;
        public UserPassController(PassMenDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<UserPass>> Get() =>
         await _context.UserPasses.ToListAsync();

        [HttpGet("id/{id}")]
        [ProducesResponseType(typeof(UserPass), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var userpass = await _context.UserPasses.FindAsync(id);
            return userpass == null ? NotFound() : Ok(userpass);
        }

        [HttpGet("username/{username}")]
        [ProducesResponseType(typeof(UserPass), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByName(string username)
        {
            var userpass = await _context.UserPasses.Where(x=>x.Username == username).FirstOrDefaultAsync();
            return userpass == null ? NotFound() : Ok(userpass);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(UserPass userpass)
        {
            await _context.UserPasses.AddAsync(userpass);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = userpass.Id }, userpass);
        }

        [HttpPut ("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, UserPass userpass)
        {
            if (id != userpass.Id) return BadRequest();
            _context.Entry(userpass).State = EntityState.Modified;

            await _context.SaveChangesAsync(); 
            return NoContent();
        }
    }
}
