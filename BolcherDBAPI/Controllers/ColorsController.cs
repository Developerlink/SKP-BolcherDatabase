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
    public class ColorsController : ControllerBase
    {
        private readonly IColorRepository _colorRepository;

        public ColorsController(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        // GET: api/Colors
        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var colors = await _colorRepository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(colors);
        }

        // GET: api/Colors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetColor(int id)
        {
            if (!await _colorRepository.ExistsAsync(id))
                return NotFound();

            var color = await _colorRepository.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(color);
        }

        // PUT: api/Colors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor(int id, Color color)
        {
            if (color == null)
                return BadRequest(ModelState);
            if (id != color.Id)
                return BadRequest();
            if (!await _colorRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _colorRepository.UpdateAsync(color))
            {
                ModelState.AddModelError("", "Something went wrong updating the color.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // POST: api/Colors
        [HttpPost]
        public async Task<IActionResult> PostColor(Color color)
        {
            if (color == null)
                return BadRequest(ModelState);
            if (await _colorRepository.ExistsAsync(color.Id))
                ModelState.AddModelError("", $"Color with id '{color.Id}' already exists.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _colorRepository.AddAsync(color))
            {
                ModelState.AddModelError("", "Something went wrong adding the color");
                return StatusCode(500, ModelState);                    
            }

            return CreatedAtAction("GetColor", new { id = color.Id }, color);
        }

        // DELETE: api/Colors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            if (!await _colorRepository.ExistsAsync(id))
                return NotFound();

            if(!await _colorRepository.DeleteAsync(id))
            {
                ModelState.AddModelError("", "Something went wrong deleting the color.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
