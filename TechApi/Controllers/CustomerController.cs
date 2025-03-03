using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechApi.DTOs;
using TechApi.Model;
using TechApi.Models;
using TechApi.Repositories.Implementation;
using TechApi.Repositories.Interface;

namespace TechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

            private readonly ICustomerRepositories customerRepository;

            public CustomerController(ICustomerRepositories customerRepository)
            {
                this.customerRepository = customerRepository;
            }


            [HttpPost]
            // [Authorize(Roles = "Writer")]
            public async Task<IActionResult> CreateInventory([FromBody] CreateCustomerDto request)
            {
                var customer = new Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Phone = request.Phone,
                    RegistrationDate = request.RegistrationDate,

                };

            customer = await customerRepository.CreateAsync(customer);

                var response = new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    RegistrationDate = customer.RegistrationDate,

                };

                return Ok(response);

            }

            [HttpGet]
            public async Task<IActionResult> GetAllInventory()
            {
                var customers = await customerRepository.GetAllAsync();

                var response = new List<CustomerDto>();
                foreach (var customer in customers)
                {
                    response.Add(new CustomerDto
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        Phone = customer.Phone,
                        RegistrationDate = customer.RegistrationDate,

                    });
                }

                return Ok(response);

            }

            [HttpGet]
            [Route("{id:int}")]
            public async Task<IActionResult> GetInventoryInventoryById([FromRoute] int id)
            {
                var customer = await customerRepository.GetByIdAsync(id);

                if (customer == null)
                {
                    return NotFound();
                }

                //convert domain model to DTO
                var response = new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    RegistrationDate = customer.RegistrationDate,
                };
            return Ok(response);
            }





            [HttpPut]
            [Route("{id:int}")]
            public async Task<IActionResult> UpdateInventoryById([FromRoute] int id, CreateCustomerDto request)
            {
            var customer = new Customer
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                RegistrationDate = request.RegistrationDate,

            };

            var customerUbdate = await customerRepository.UpdateAsync(customer);

                if (customerUbdate == null)
                {
                    return NotFound();
                }

            var response = new CustomerDto
            {
                Id = customerUbdate.Id,
                FirstName = customerUbdate.FirstName,
                LastName = customerUbdate.LastName,
                Email = customerUbdate.Email,
                Phone = customerUbdate.Phone,
                RegistrationDate = customerUbdate.RegistrationDate,
            };

            return Ok(response);
            }


            [HttpDelete]
            [Route("{id:int}")]
            //   [Authorize(Roles = "Writer")]
            public async Task<IActionResult> DeleteInventory([FromRoute] int id)
            {
                var deleteCustomer = await customerRepository.DeleteAsync(id);

                if (deleteCustomer == null) { return NotFound(); }

                //convert Domain model to DTO
            var response = new CustomerDto
            {
                Id = deleteCustomer.Id,
                FirstName = deleteCustomer.FirstName,
                LastName = deleteCustomer.LastName,
                Email = deleteCustomer.Email,
                Phone = deleteCustomer.Phone,
                RegistrationDate = deleteCustomer.RegistrationDate,
            };

                return Ok(response);

            }
        }
    }

