using Customer.Application.DTOs;
using Customer.Application.Services;
using Customer.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateCustomerRequest request)
        {
            var result = await _customerService.Register(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _customerService.Login(request);
            return result.IsSuccess ? Ok(result) : Unauthorized(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(uint id)
        {
            var result = await _customerService.Delete(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerRequest request)
        {
            var result = await _customerService.Update(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            if (result.Message == "User Not Found")
            {
                return NotFound(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(uint id)
        {
            var result = await _customerService.GetById(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerService.GetAll();
            return Ok(result);
        }
    }
}