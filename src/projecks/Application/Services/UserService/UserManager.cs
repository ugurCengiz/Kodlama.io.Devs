using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.Security.Entities;

namespace Application.Services.UserService
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByMail(string email)
        {
            User user = await _userRepository.GetAsync(x => x.Email == email);
            return user;
        }
    }
}
