using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nhom9.Docker_Kubernet.Demo.DL.Repositories;
using Nhom9.Docker_Kubernet.Demo.Entity.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9.Docker_Kubernet.Demo.Admin.Utils
{
    public class Authenticator
    {
        private readonly IConfiguration _config;
        private readonly UserRepository _userRepository;

        public Authenticator(IWebHostEnvironment env, UserRepository userRepository)
        {
            _config = Utils.Environment.GetConfiguration(env);
            _userRepository = userRepository;
        }

        public User AuthenticateUser(User loginCredentials)
        {
            User user = _userRepository.GetUserByCredentials(loginCredentials.Username, loginCredentials.Password);
            return user;
        }

        public string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim("fullname", userInfo.Fullname.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt: Issuer"],
                audience: _config["Jwt: Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
