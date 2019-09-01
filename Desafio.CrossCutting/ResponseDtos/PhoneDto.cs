using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.CrossCutting.ResponseDtos
{
    public class PhoneDto
    {
        public string Number { get; set; }
        public int AreaCode { get; set; }
        public string CountryCode { get; set; }
    }
}
