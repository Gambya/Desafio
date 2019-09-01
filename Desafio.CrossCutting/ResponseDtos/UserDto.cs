using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.CrossCutting.ResponseDtos
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public IEnumerable<PhoneDto> Phones { get; set; }
    }
}
