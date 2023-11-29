using Auth.Business.Abstract;
using Auth.DataAccess.Abstract;
using Auth.DataAccess.Concrete;
using Auth.Entities;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;

        private readonly IConfiguration _configuration;

        public UserManager(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }





       
        public  string? AuthenticateUser(User user)
        {
            return  GenerateJwtToken(user.username);
            
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!); // appsettings.json dosyasındaki gizli anahtarı al
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username),
                    // İsteğe bağlı: Diğer istenen iddiaları (claims) ekleyebilirsiniz
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token süresi (1 saat)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }




        public async Task<User> CreateUser(User user)
        {

            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(user.password);

            user.password = hashedPassword;

            return await _userRepository.CreateUser(user);


        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }

        
    }
}
