using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Domain.Entities
{
    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int AreaCode { get; set; }
        public string CountryCode { get; set; }
        public User User { get; set; }
    }
}
