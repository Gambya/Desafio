using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Models;
using Desafio.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [Authorize("Bearer")]
        [HttpGet("me")]
        public ActionResult GetUser([FromHeader] string email)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = _userService.GetUser(email);

            if (user == null) return Ok(new { message = "Nonexistent user", errorCode = 200 });

            return Ok(new MeModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phones = user.Phones.Select(p => new PhoneModel
                {
                    Area_Code = p.AreaCode,
                    Country_Code = p.CountryCode,
                    Number = p.Number
                }),
                Created_at = user.CreatedAt,
                Last_login = (DateTime.MinValue == user.LastLogin) ? null : user.LastLogin
            });
        }
    }
}