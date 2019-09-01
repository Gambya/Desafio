using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Api.Models
{
    public class MeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<PhoneModel> Phones { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Last_login { get; set; }
    }
}
