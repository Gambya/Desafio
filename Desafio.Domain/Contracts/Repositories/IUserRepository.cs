using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Contracts
{
    public interface IUserRepository
    {
        Task InsertUser(User user);
        User Find(string email);
        void UpdateLogin(User user);
    }
}
