using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.CrossCutting.Helpers
{
    public class TokenConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiracaoTokenHoras { get; set; }
    }
}
