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
            var strengths = await _strengthRepository.GetAllAsync();

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

            var strength = await _strengthRepository.GetByIdAsync(id);

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _strengthRepository.UpdateAsync(strength);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);                                
            }

            return NoContent();
        }

        // POST: api/Strengths
        [HttpPost]
        public async Task<IActionResult> PostStrength(Strength strength)
        {
            if (strength == null)
                return BadRequest(ModelState);
            if (await _strengthRepository.ExistsAsync(strength.Id))
                ModelState.AddModelError("", "A strength with that id already exists.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _strengthRepository.AddAsync(strength);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }
            
            return CreatedAtAction("GetStrength", new { id = strength.Id }, strength);
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
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
