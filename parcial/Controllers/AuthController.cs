using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using JwtExample.Models;

namespace JwtExample.Controllers
{
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    // GET api/values
    [HttpPost, Route("login")]
    public IActionResult Login([FromBody]LoginModel user)
    {
        if (user == null)
        {
            return BadRequest("Invalid client request");
        }
        if (user.UserName == "paola" && user.Password == "qwert")
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@3451"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
        }
        else
        {
            return Unauthorized();
        }
    }
}
}