using Desafio.CrossCutting.Helpers;
using Desafio.CrossCutting.ResponseDtos;
using Desafio.Domain.Contracts;
using Desafio.Domain.Entities;
using Desafio.Services.Validators;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenConfig _tokenConfig;
        private readonly SigningConfig _signingConfig;

        public AuthService(IUserRepository userRepository, TokenConfig tokenConfig, SigningConfig signingConfig)
        {
            _userRepository = userRepository;
            _tokenConfig = tokenConfig;
            _signingConfig = signingConfig;
        }

        public ResponseLoginDto Authenticate(User user)
        {
            try
            {
                Validate(user, Activator.CreateInstance<LoginValidator>());


                User userBase = _userRepository.Find(user.Email);
                var credenciaisValidas = (userBase != null &&
                        user.Email == userBase.Email &&
                        user.Password == userBase.Password);

                if (!credenciaisValidas)
                    return new ResponseLoginDto
                    {
                        Succeeded = false,
                        Message = "Invalid e-mail or password",
                        ErrorCode = 400
                    };

                ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.Email, "Login"),
                        new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                        });

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromHours(_tokenConfig.ExpiracaoTokenHoras);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfig.Issuer,
                    Audience = _tokenConfig.Audience,
                    SigningCredentials = _signingConfig.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });

                var token = handler.WriteToken(securityToken);

                _userRepository.UpdateLogin(user);

                return new ResponseLoginDto
                {
                    Succeeded = true,
                    Created = userBase.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    Token = token,
                    Message = "User successfully logged in!",
                    Login = new UserDto
                    {
                        FirstName = userBase.FirstName,
                        LastName = userBase.LastName,
                        Email = userBase.Email,
                        Password = "*****",
                        CreatedAt = userBase.CreatedAt,
                        LastLogin = null,
                        Phones = userBase.Phones.Select(p => new PhoneDto
                        {
                            AreaCode = p.AreaCode,
                            CountryCode = p.CountryCode,
                            Number = p.Number
                        }),
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseLoginDto
                {
                    Succeeded = false,
                    Message = ex.Message,
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
