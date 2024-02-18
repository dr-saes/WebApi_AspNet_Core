﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApi_AspNet_Core;

[ApiController]
[Route("webapi-aspnet-core/account/")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public AuthController(SignInManager<IdentityUser> signInManager,
                            UserManager<IdentityUser> useManager,
                            IOptions<JwtSettings> jwtSettings)
    {
        _signInManager = signInManager;
        _userManager = useManager;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterUserViewModel registerUser)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, registerUser.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Ok(await GenerateJwt(user.Email));
        }
        return Problem("Failed to register user.");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserViewModel loginUser)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
        if (result.Succeeded)
        {
            return Ok(await GenerateJwt(loginUser.Email));
        }
        return Problem("Incorrect username and/ or password.");
    }

    private async Task<string> GenerateJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);
        return encodedToken;

    }
}




