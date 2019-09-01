using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.CrossCutting.ResponseDtos
{
    public class ResponseLoginDto
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string Message { get; set; }
        public UserDto Login { get; set; }
        public int ErrorCode { get; set; }
    }
}
