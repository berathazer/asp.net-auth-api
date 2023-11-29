using Auth.Business.Abstract;
using Auth.Business.Concrete;
using Auth.DataAccess.Abstract;
using Auth.Entities;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/users")]
    [ApiController]

    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
      
                return Ok(await _userService.GetAllUsers());
                
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {

            if (ModelState.IsValid)
            {
                return Ok(await _userService.CreateUser(user));
            }
            else
            {
                return BadRequest("Girilen alanlar hatalı tekrar deneyiniz.");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] User user) 
        
        {
            if (ModelState.IsValid)
            {
                var _user = await _userService.GetUserByUsername(user.username);

                if(_user != null && BCrypt.Net.BCrypt.EnhancedVerify(user.password, _user.password)) {

                    var token = _userService.AuthenticateUser(user);

                    return Ok(new
                    {
                        username = user.username,
                        token = token
                    });
                }
                else
                {
                    return NotFound("Kullanıcı bulunamadı.");
                }
            }
            else
            {
                return BadRequest("Girilen alanlar hatalı tekrar deneyiniz.");
            }
        }


    }
}
