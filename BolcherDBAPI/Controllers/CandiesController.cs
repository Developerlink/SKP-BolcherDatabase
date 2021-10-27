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

        [HttpGet("color")]
        public async Task<IActionResult> GetByColor([FromQuery] int? color_id, int? color2_id, int? not_color_id)
        {
            ICollection<Candy> candies = null;

            if (not_color_id.HasValue)
            {
                candies = await _candyRepository.GetCandiesNotOfColor(not_color_id.Value);
            }
            else if (color_id.HasValue && color2_id.HasValue)
            {
                candies = await _candyRepository.GetCandiesByTwoColorsAsync(color_id.Value, color2_id.Value);
            }
            else if (color_id.HasValue)
            {
                candies = await _candyRepository.GetCandiesByColorAsync(color_id.Value);
            }
            else
            {
                ModelState.AddModelError("errors", "The request does not contain any color id's.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(candies);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBySearch([FromQuery] string starts_with, string contains)
        {
            ICollection<Candy> candies = null;

            if (starts_with != null)
            {
                candies = await _candyRepository.GetBySearchStartsWithAsync(starts_with);
            }
            else if (contains != null)
            {
                candies = await _candyRepository.GetBySearchContainsAsync(contains);
            }
            else
            {
                ModelState.AddModelError("errors", "The request does not contain any searh terms");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(candies);
        }

        [HttpGet("weight")]
        public async Task<IActionResult> GetByWeight([FromQuery] int? lower_limit, int? upper_limit)
        {
            ICollection<Candy> candies = null;

            if (lower_limit.HasValue && upper_limit.HasValue)
            {
                candies = await _candyRepository.GetByWeightBetween(lower_limit.Value, upper_limit.Value);
            }
            else if (upper_limit.HasValue)
            {
                candies = await _candyRepository.GetByWeightLowerThan(upper_limit.Value);
            }
            else
            {
                ModelState.AddModelError("errors", "The request does not contain any weight limits.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(candies);
        }

        [HttpGet("weight/heaviest")]
        public async Task<IActionResult> GetHeaviest([FromQuery]int take_amount)
        {
            ICollection<Candy> candies = null;

            try
            {
                candies = await _candyRepository.GetHeaviestBy(take_amount);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(candies);
        }

        [HttpGet("random")]
        public IActionResult GetRandom()
        {
            var candy = _candyRepository.GetRandomCandy();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(candy);
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
            if (!_candyRepository.HasUniqueName(id, candy.Name))
            {
                ModelState.AddModelError("errors", "That name already exists.");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _candyRepository.UpdateAsync(candy);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
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
            {
                ModelState.AddModelError("errors", "That id already exists.");
                return StatusCode(409, ModelState);
            }
            if (!_candyRepository.HasUniqueName(candy.Id, candy.Name))
            {
                ModelState.AddModelError("errors", "That name already exists.");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _candyRepository.AddAsync(candy);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
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
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
