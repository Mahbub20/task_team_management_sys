using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using taskTeamManagementSystem.Data;
using taskTeamManagementSystem.Dtos;
using taskTeamManagementSystem.Helpers;
using taskTeamManagementSystem.Models;

namespace taskTeamManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TaskDBContext _context;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly PasswordHasher<User> _hasher;

        public AuthController(TaskDBContext context, JwtTokenGenerator tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
            _hasher = new PasswordHasher<User>();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.Include(u => u.Role).SingleOrDefault(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Invalid credentials");

            var token = _tokenGenerator.GenerateToken(user);
            return Ok(new { token });
        }
    }
}