using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BolcherDBModelLibrary;
using BolcherDBDataAccessLibrary;

namespace BolcherDbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SournessesController : ControllerBase
    {
        private readonly BolcherDBContext _context;

        public SournessesController(BolcherDBContext context)
        {
            _context = context;
        }

        // GET: api/Sournesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sourness>>> GetSournesses()
        {
            return await _context.Sournesses.ToListAsync();
        }

        // GET: api/Sournesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sourness>> GetSourness(int id)
        {
            var sourness = await _context.Sournesses.FindAsync(id);

            if (sourness == null)
            {
                return NotFound();
            }

            return sourness;
        }

        // PUT: api/Sournesses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSourness(int id, Sourness sourness)
        {
            if (id != sourness.Id)
            {
                return BadRequest();
            }

            _context.Entry(sourness).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SournessExists(id))
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

        // POST: api/Sournesses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sourness>> PostSourness(Sourness sourness)
        {
            _context.Sournesses.Add(sourness);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSourness", new { id = sourness.Id }, sourness);
        }

        // DELETE: api/Sournesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSourness(int id)
        {
            var sourness = await _context.Sournesses.FindAsync(id);
            if (sourness == null)
            {
                return NotFound();
            }

            _context.Sournesses.Remove(sourness);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SournessExists(int id)
        {
            return _context.Sournesses.Any(e => e.Id == id);
        }
    }
}
