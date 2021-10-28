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
            ICollection<Sourness> sournesses;

            try
            {
                sournesses = await _sournessRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

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

            Sourness sourness;

            try
            {
                sourness = await _sournessRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);                
            }

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
            if (!_sournessRepository.HasUniqueName(id, sourness.Name))
            {
                ModelState.AddModelError("errors", "That name already exists.");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // POST: api/Sournesses
        [HttpPost]
        public async Task<IActionResult> PostSourness(Sourness newSourness)
        {
            if (newSourness == null)
                return BadRequest(ModelState);
            if (await _sournessRepository.ExistsAsync(newSourness.Id))
            {
                ModelState.AddModelError("errors", "That id already exists.");
                return StatusCode(409, ModelState);
            }
            if (!_sournessRepository.HasUniqueName(newSourness.Id, newSourness.Name))
            {
                ModelState.AddModelError("errors", "That name already exists.");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _sournessRepository.AddAsync(newSourness);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetSourness", new { id = newSourness.Id }, newSourness);
        }

        // DELETE: api/Sournesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSourness(int id)
        {
            if (!await _sournessRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _sournessRepository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
