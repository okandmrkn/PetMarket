using Customer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Application.DTOs
{
    public class UpdateCustomerRequest
    {
  
        public uint Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public byte Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        
    }
}
