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
    public class StrengthsController : ControllerBase
    {
        private readonly IStrengthRepository _strengthRepository;

        public StrengthsController(IStrengthRepository strengthRepository)
        {
            _strengthRepository = strengthRepository;
        }

        // GET: api/Strengths
        [HttpGet]
        public async Task<IActionResult> GetStrengths()
        {
            ICollection<Strength> strengths;

            try
            {
                strengths = await _strengthRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(strengths);
        }

        // GET: api/Strengths/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStrength(int id)
        {
            if (!await _strengthRepository.ExistsAsync(id))
                return NotFound();

            Strength strength;

            try
            {
                strength = await _strengthRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(strength);
        }

        // PUT: api/Strengths/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStrength(int id, Strength strength)
        {
            if (strength == null)
                return BadRequest(ModelState);
            if (id != strength.Id)
                return BadRequest();
            if (!await _strengthRepository.ExistsAsync(id))
                NotFound();
            if (!_strengthRepository.HasUniqueName(id, strength.Name))
            {
                ModelState.AddModelError("errors", "That name already exists.");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _strengthRepository.UpdateAsync(strength);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);                                
            }

            return NoContent();
        }

        // POST: api/Strengths
        [HttpPost]
        public async Task<IActionResult> PostStrength(Strength newStrength)
        {
            if (newStrength == null)
                return BadRequest(ModelState);
            if (await _strengthRepository.ExistsAsync(newStrength.Id))
            {
                ModelState.AddModelError("errors", "That id already exists.");
                return StatusCode(409, ModelState);
            }
            var strength = await _strengthRepository.GetByIdAsync(newStrength.Id);
            if(!_strengthRepository.HasUniqueName(newStrength.Id, newStrength.Name))
            {
                    ModelState.AddModelError("errors", "That name already exists.");
                    return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _strengthRepository.AddAsync(newStrength);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }
            
            return CreatedAtAction("GetStrength", new { id = newStrength.Id }, newStrength);
        }

        // DELETE: api/Strengths/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStrength(int id)
        {
            if (!await _strengthRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _strengthRepository.DeleteAsync(id);
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
