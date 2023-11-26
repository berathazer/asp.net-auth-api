using Auth.Business.Abstract;
using Auth.DataAccess.Abstract;
using Auth.DataAccess.Concrete;
using Auth.Entities;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
