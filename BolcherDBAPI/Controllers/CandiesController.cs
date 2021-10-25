using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BolcherDBDataAccessLibrary;
using BolcherDBModelLibrary;
using BolcherDBModelLibrary.Interfaces;

namespace BolcherDbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandiesController : ControllerBase
    {
        private readonly ICandyRepository _candyRepository;

        public CandiesController(ICandyRepository candyRepository)
        {
            _candyRepository = candyRepository;
        }

        // GET: api/Candies
        [HttpGet]
        [ResponseCache(Duration = 300)]
        public async Task<IActionResult> GetCandies()
        {
            var candies = await _candyRepository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(candies);
        }

        // GET: api/Candies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandy(int id)
        {
            if (!await _candyRepository.ExistsAsync(id))
                return NotFound();

            var candy = await _candyRepository.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(candy);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBySearchStartsWith([FromQuery]string searchTerm)
        {
            if (searchTerm == null)
                return BadRequest(ModelState);

            var candies = await _candyRepository.GetBySearchStartsWithAsync(searchTerm);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(candies);
        }

        // PUT: api/Candies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandy(int id, Candy candy)
        {
            if (candy == null)
                return BadRequest(ModelState);
            if (id != candy.Id)
                return BadRequest();
            if (!await _candyRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _candyRepository.UpdateAsync(candy);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // POST: api/Candies
        [HttpPost]
        public async Task<IActionResult> PostCandy(Candy candy)
        {
            if (candy == null)
                return BadRequest(ModelState);
            if (await _candyRepository.ExistsAsync(candy.Id))
                ModelState.AddModelError("", $"Candy with id '{candy.Id}' already exists.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _candyRepository.AddAsync(candy);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetCandy", new { id = candy.Id }, candy);
        }

        // DELETE: api/Candies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandy(int id)
        {
            if (!await _candyRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _candyRepository.DeleteAsync(id);
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
