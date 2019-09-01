using Desafio.Domain.Contracts;
using Desafio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DesafioDbContext _dbContext;
        public UserRepository(DesafioDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Find(string email)
        {
            return _dbContext.Users.Include(u => u.Phones).FirstOrDefault(u => u.Email == email);
        }

        public async Task InsertUser(User user)
        {
            user.CreatedAt = DateTime.Now;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateLogin(User user)
        {
            var userBase = _dbContext.Users.Where(u => u.Email == user.Email).FirstOrDefault();

            // Validate entity is not null
            if (userBase != null)
            {
                userBase.LastLogin = DateTime.Now;
                _dbContext.Users.Update(userBase);
                _dbContext.SaveChanges();
            }
        }
    }
}
