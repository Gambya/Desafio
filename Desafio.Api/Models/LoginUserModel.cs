using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Api.Models
{
    public class LoginUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
