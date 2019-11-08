using AutoMapper;
using Prac1Proj.DTO;
using Prac1Proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Prac1Proj.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ProjectContext _context;
        public CustomersController()
        {
            _context = new ProjectContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //GET /api/customers
        public IEnumerable<CustomerDTO> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map< Customer, CustomerDTO>);
        }
        //GET /api/customers/{id}
        public IHttpActionResult GetCustomers(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer,CustomerDTO>(customer));         
        }
        //POST /api/customers/
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = Mapper.Map<CustomerDTO, Customer>(customerDTO);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDTO.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" +customer.Id),customerDTO);
        }
        [HttpPut]
        //PUT /api/customers/{id}
        public void UpdateCustomer(int id,CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map(customerDTO, customerInDb);
            _context.SaveChanges();
        }
        [HttpDelete]
        //GET /api/customers/{id}
        public void DeleteCustomers(int id )
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

    }
}
