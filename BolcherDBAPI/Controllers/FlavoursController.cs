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
    public class FlavoursController : ControllerBase
    {
        private readonly IFlavourRepository _flavourRepository;

        public FlavoursController(IFlavourRepository flavourRepository)
        {
            _flavourRepository = flavourRepository;
        }

        // GET: api/Flavours
        [HttpGet]
        public async Task<IActionResult> GetFlavours()
        {
            var flavours = await _flavourRepository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flavours);
        }

        // GET: api/Flavours/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlavour(int id)
        {
            if (!await _flavourRepository.ExistsAsync(id))
                return NotFound();

            var flavour = await _flavourRepository.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flavour);
        }

        // PUT: api/Flavours/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlavour(int id, Flavour flavour)
        {
            if (flavour == null)
                return BadRequest(ModelState);
            if (id != flavour.Id)
                return BadRequest();
            if (!await _flavourRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _flavourRepository.UpdateAsync(flavour);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // POST: api/Flavours
        [HttpPost]
        public async Task<IActionResult> PostFlavour(Flavour flavour)
        {
            if (flavour == null)
                return BadRequest(ModelState);
            if (await _flavourRepository.ExistsAsync(flavour.Id))
                ModelState.AddModelError("", "A flavour with that id already exists");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _flavourRepository.AddAsync(flavour);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetFlavour", new { id = flavour.Id }, flavour);
        }

        // DELETE: api/Flavours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlavour(int id)
        {
            if (!await _flavourRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _flavourRepository.DeleteAsync(id);
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
