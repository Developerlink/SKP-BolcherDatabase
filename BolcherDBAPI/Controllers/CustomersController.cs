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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICandyRepository _candyRepository;
        private readonly IStrengthRepository _strengthRepository;

        public CustomersController(ICustomerRepository customerRepository, 
            ICandyRepository candyRepository,
            IStrengthRepository strengthRepository)
        {
            _customerRepository = customerRepository;
            _candyRepository = candyRepository;
            _strengthRepository = strengthRepository;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] string has_orders, string above100, int? above_amount)
        {
            ICollection<Customer> customers;

            try
            {
                if (has_orders != null && has_orders == "true")
                {
                    if (above100 != null && above100 == "true")
                    {
                        customers = new List<Customer>();
                        var preCustomers = await _customerRepository.GetCustomersWithSalesOrders();

                        foreach (var customer in preCustomers)
                        {
                            int sumOfWeightOfBoughtCandies = 0;
                            foreach (var salesOrder in customer.SalesOrders)
                            {
                                foreach (var orderLine in salesOrder.OrderLines)
                                {
                                    sumOfWeightOfBoughtCandies += orderLine.Candy.Weight * orderLine.Amount;
                                }
                            }
                            if (sumOfWeightOfBoughtCandies > 100)
                            {
                                customers.Add(customer);
                            }
                        }
                    }
                    else if (above_amount.HasValue)
                    {
                        customers = new List<Customer>();
                        var preCustomers = await _customerRepository.GetCustomersWithSalesOrders();

                        foreach (var customer in preCustomers)
                        {
                            double sumMoneySpend = 0;
                            foreach (var salesOrder in customer.SalesOrders)
                            {
                                foreach (var orderLine in salesOrder.OrderLines)
                                {
                                    sumMoneySpend += (orderLine.Candy.ProductionCost*3.5)*1.25 * orderLine.Amount;
                                }
                            }
                            if (sumMoneySpend > above_amount*100)
                            {
                                customers.Add(customer);
                            }
                        }
                    }
                    else
                    {
                        customers = await _customerRepository.GetCustomersWithSalesOrders();
                    }
                }
                else
                {
                    customers = await _customerRepository.GetAllAsync();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return Ok(customers);
        }

        [HttpGet("candy/{candyId}")]
        public async Task<IActionResult> GetCustomersByCandyId(int candyId)
        {
            if (!await _candyRepository.ExistsAsync(candyId))
            {
                ModelState.AddModelError("errors", "That candy's id does not exist.");
                return StatusCode(404, ModelState);
            }

            ICollection<Customer> customers;

            try
            {
                customers = await _customerRepository.GetCustomersWhoBoughtSpecificCandy(candyId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return Ok(customers);
        }

        [HttpGet("candy/strength/{strengthId}")]
        public async Task<IActionResult> GetCustomersByCandyStrengthId(int strengthId)
        {
            if (!await _strengthRepository.ExistsAsync(strengthId))
            {
                ModelState.AddModelError("errors", "That strength's id does not exist.");
                return StatusCode(404, ModelState);
            }

            ICollection<Customer> customers;

            try
            {
                customers = await _customerRepository.GetCustomersWhoBoughtCandyWithStrength(strengthId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            if (!await _customerRepository.ExistsAsync(id))
                return NotFound();

            Customer customer;

            try
            {
                customer = await _customerRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("errors", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (customer == null)
                return BadRequest(ModelState);
            if (id != customer.Id)
                return BadRequest();
            if (!await _customerRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _customerRepository.UpdateAsync(customer);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customer customer)
        {
            if (customer == null)
                return BadRequest(ModelState);
            if (await _customerRepository.ExistsAsync(customer.Id))
                ModelState.AddModelError("", "Customer with that id already exists");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _customerRepository.AddAsync(customer);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!await _customerRepository.ExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _customerRepository.DeleteAsync(id);
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
