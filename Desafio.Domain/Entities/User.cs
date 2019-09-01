using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public ICollection<Phone> Phones { get; set; }
    }
}
