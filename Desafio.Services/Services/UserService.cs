using Desafio.CrossCutting.Helpers;
using Desafio.CrossCutting.ResponseDtos;
using Desafio.Domain.Contracts;
using Desafio.Domain.Entities;
using Desafio.Services.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(string email)
        {
            return _userRepository.Find(email);
        }

        public bool IsEmailUnique(string email)
        {
            if (_userRepository.Find(email) == null) return true;

            return false;
        }

        public async Task<ResponseAuthDto> RegistrarUser(User user)
        {
            try
            {

                Validate(user, Activator.CreateInstance<UserValidator>());

                if (!IsEmailUnique(user.Email))
                    return new ResponseAuthDto
                    {
                        Succeeded = false,
                        Message = "E-mail already exists",
                        ErrorCode = 400
                    };

                await _userRepository.InsertUser(user);

                return new ResponseAuthDto
                {
                    Succeeded = true,
                    Message = "User successfully registered!"
                };
            }
            catch (Exception ex)
            {
                return new ResponseAuthDto
                {
                    Succeeded = false,
                    Message = CleamMessages.CleamMessage(ex.Message),
                    ErrorCode = 400
                };
            }
        }

        private void Validate(User user, AbstractValidator<User> validator)
        {
            if (user == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(user);
        }
    }
}
