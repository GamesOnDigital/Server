using BLL.Interfaces;
using DAL.Models;
using DTO.Classes;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts.PatternContextLinear;
using User = DTO.Classes.User;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly JwtTokenGenerator JwtTokenGenerator;
        private readonly IUserBll _Users;

        public UserController(IUserBll Users, JwtTokenGenerator jwtTokenGenerator)
        {
            _Users = Users;
            this.JwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(User User1)
        {

            User user =await _Users.SignIn(User1);
            if (user != null)
            {
                var token = JwtTokenGenerator.GenerateToken(user.Email + '#' + user.FirstName);
                return Ok(new { token = token, user = user });
            }
            return Unauthorized();
        }

        [HttpPut("{id}")]
        public async Task<User> Update(User updatedUser)
        {
            return await _Users.Update(updatedUser);

        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _Users.Delete(id);
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery]string email, [FromQuery] string password)
        {            
            var user = await _Users.Login(email, password);
            if (user != null)
            {
                var token = JwtTokenGenerator.GenerateToken(user.Email+'#'+user.FirstName);
                return Ok(new { token = token, user= user });
            }
            return Unauthorized();
        }

        [HttpGet()]
        public async Task<List<User>> GetAll()
        {
            return await _Users.GetAll();
        }
    }

}

