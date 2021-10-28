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
using BolcherDBAPI.Extensions;

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
            ICollection<Color> colors;

            try
            {
                colors = await _colorRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

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

            Color color;

            try
            {
                color = await _colorRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

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
            if (!_colorRepository.HasUniqueName(id, color.Name))
            {
                ModelState.AddModelError("errors", "That name already exists.");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _colorRepository.UpdateAsync(color);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
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
            {
                ModelState.AddModelError("errors", "That ID already exists.");
                return StatusCode(409, ModelState);
            }
            if (!_colorRepository.HasUniqueName(color.Id, color.Name))
            {
                ModelState.AddModelError("errors", "That name already exists.");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _colorRepository.AddAsync(color);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetOriginalException().Message);
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

            try
            {
                await _colorRepository.DeleteAsync(id);
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
