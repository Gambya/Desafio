using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Models;
using Desafio.Domain.Contracts;
using Desafio.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public ActionResult Login(LoginUserModel loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = _authService.Authenticate(new User
            {
                Email = loginUser.Email,
                Password = loginUser.Password
            });

            if (!result.Succeeded)
                return BadRequest(new { message = result.Message, errorCode = result.ErrorCode });

            return Ok(new UserModel
            {
                FirstName = result.Login.FirstName,
                LastName = result.Login.LastName,
                Email = result.Login.Email,
                Phones = result.Login.Phones.Select(p => new PhoneModel
                {
                    Area_Code = p.AreaCode,
                    Country_Code = p.CountryCode,
                    Number = p.Number
                }),
                Created_at = result.Login.CreatedAt,
                Last_login = (DateTime.MinValue == result.Login.LastLogin) ? null : result.Login.LastLogin,
                Token = result.Token
            });
        }
    }
}