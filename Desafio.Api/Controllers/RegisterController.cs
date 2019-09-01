using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Models;
using Desafio.CrossCutting.ResponseDtos;
using Desafio.Domain.Contracts;
using Desafio.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService) => _userService = userService;

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<ActionResult> RegisterUser(RegisterUserModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _userService.RegistrarUser(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Phones = user.Phones.Select(p => new Phone
                {
                    Number = p.Number,
                    AreaCode = p.Area_Code,
                    CountryCode = p.Country_Code
                }).ToList()
            });

            if (!result.Succeeded)
                return BadRequest(new { message = result.Message, errorCode = result.ErrorCode });

            return Ok(result);
        }
    }
}