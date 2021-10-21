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
    public class SalesOrdersController : ControllerBase
    {
        private readonly ISalesOrderRepository _salesOrderRepository;

        public SalesOrdersController(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }

        // GET: api/SalesOrders
        [HttpGet]
        public async Task<IActionResult> GetSalesOrders()
        {
            var salesOrders = await _salesOrderRepository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(salesOrders);
        }

        // GET: api/SalesOrders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesOrder(int id)
        {
            if (!await _salesOrderRepository.ExistsAsync(id))
                return NotFound();

            var salesOrder = await _salesOrderRepository.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(salesOrder);
        }

        // PUT: api/SalesOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesOrder(int id, SalesOrder salesOrder)
        {
            if (salesOrder == null)
                return BadRequest(ModelState);
            if (id != salesOrder.Id)
                return BadRequest();
            if (!await _salesOrderRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _salesOrderRepository.UpdateAsync(salesOrder))
            {
                ModelState.AddModelError("", "Something went wrong updating the sales order");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // POST: api/SalesOrders
        [HttpPost]
        public async Task<IActionResult> PostSalesOrder(SalesOrder salesOrder)
        {
            if (salesOrder == null)
                return BadRequest(ModelState);
            if (await _salesOrderRepository.ExistsAsync(salesOrder.Id))
                ModelState.AddModelError("", "A sales order with that id already exists");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _salesOrderRepository.AddAsync(salesOrder))
            {
                ModelState.AddModelError("", "Something went wrong adding the sales order.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetSalesOrder", new { id = salesOrder.Id }, salesOrder);
        }

        // DELETE: api/SalesOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOrder(int id)
        {
            if (!await _salesOrderRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _salesOrderRepository.DeleteAsync(id))
            {
                ModelState.AddModelError("", "Something went wrong deleting the sales order.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
