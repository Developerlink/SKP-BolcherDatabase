using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BolcherDBModelLibrary;
using BolcherDBDataAccessLibrary;
using BolcherDBModelLibrary.Interfaces;

namespace BolcherDbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SournessesController : ControllerBase
    {
        private readonly ISournessRepository _sournessRepository;

        public SournessesController(ISournessRepository sournessRepository)
        {
            _sournessRepository = sournessRepository;
        }

        // GET: api/Sournesses
        [HttpGet]
        public async Task<IActionResult> GetSournesses()
        {
            var sournesses = await _sournessRepository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sournesses);
        }

        // GET: api/Sournesses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSourness(int id)
        {
            if (!await _sournessRepository.ExistsAsync(id))
                return NotFound();

            var sourness = await _sournessRepository.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sourness);
        }

        // PUT: api/Sournesses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSourness(int id, Sourness sourness)
        {
            if (sourness == null)
                return BadRequest(ModelState);
            if (id != sourness.Id)
                return BadRequest();
            if (!await _sournessRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _sournessRepository.UpdateAsync(sourness))
            {
                ModelState.AddModelError("", "Something went wrong updating the sourness");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // POST: api/Sournesses
        [HttpPost]
        public async Task<IActionResult> PostSourness(Sourness sourness)
        {
            if (sourness == null)
                return BadRequest(ModelState);
            if (await _sournessRepository.ExistsAsync(sourness.Id))
                ModelState.AddModelError("", "A sourness with that id already exists.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _sournessRepository.AddAsync(sourness))
            {
                ModelState.AddModelError("", "Something went wrong adding the sourness.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetSourness", new { id = sourness.Id }, sourness);
        }

        // DELETE: api/Sournesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSourness(int id)
        {
            if (!await _sournessRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _sournessRepository.DeleteAsync(id))
            {
                ModelState.AddModelError("", "Something went wrong deleting the sourness.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
