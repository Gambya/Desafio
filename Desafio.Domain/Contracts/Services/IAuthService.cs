using Desafio.CrossCutting.ResponseDtos;
using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Contracts
{
    public interface IAuthService
    {
        ResponseLoginDto Authenticate(User user);
    }
}
