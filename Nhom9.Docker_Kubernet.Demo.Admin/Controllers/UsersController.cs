using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Nhom9.Docker_Kubernet.Demo.Admin.Utils;
using Nhom9.Docker_Kubernet.Demo.DL.Repositories;
using Nhom9.Docker_Kubernet.Demo.Entity.DTO;
using Nhom9.Docker_Kubernet.Demo.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhom9.Docker_Kubernet.Demo.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly Authenticator _authenticator;
        private readonly UserRepository _userRepository;

        public UsersController(IWebHostEnvironment env, Authenticator authenticator, UserRepository userRepository)
        {
            _config = Utils.Environment.GetConfiguration(env);
            _authenticator = authenticator;
            _userRepository = userRepository;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            User user = _authenticator.AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = _authenticator.GenerateJWTToken(user);

                // Gói respone vào DTO

                AuthenResponeDTO authenResponeDTO = new AuthenResponeDTO
                {
                    StatusCode = Status.LoginSuccess,
                    Message = "Dang nhap thanh cong",
                    Data = new
                    {
                        token = tokenString,
                        userDetails = user,
                    }
                };
                response = Ok(authenResponeDTO);
            }
            return response;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{page}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserBySkipTake(int page)
        {
            try
            {
                var users = await _userRepository.GetSkipTake(page);
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{userID}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(string userID)
        {
            try
            {
                var users = await _userRepository.Delete(userID);
                return Ok(userID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> EditUser(User user)
        {
            try
            {
                await _userRepository.UpdateUser(user);
                return Ok(user.UserID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> InsertUser(User user)
        {
            try
            {
                await _userRepository.Insert(user);
                return Ok(user.UserID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }
    }
}
