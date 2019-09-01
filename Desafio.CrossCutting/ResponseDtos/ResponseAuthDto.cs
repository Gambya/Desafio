using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.CrossCutting.ResponseDtos
{
    public class ResponseAuthDto
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
    }
}
